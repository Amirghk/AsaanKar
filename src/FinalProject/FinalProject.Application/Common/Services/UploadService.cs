using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Enums;
using FinalProject.Application.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using FinalProject.Application.Common.ConfigurationModels;

namespace FinalProject.Application.Common.Services;

public class UploadService : IUploadService
{
    private readonly IMapper _mapper;
    private readonly IUploadRepository _repository;
    private readonly IExpertService _expertService;
    private readonly ICustomerService _customerService;
    private readonly ICommentService _commentService;
    private readonly ICategoryService _categoryService;
    private readonly AppSettings _appSettings;

    public UploadService(IUploadRepository repository,
                         IMapper mapper,
                         IExpertService expertService,
                         ICustomerService customerService,
                         ICommentService commentService,
                         ICategoryService categoryService,
                         IOptions<AppSettings> appSettings)
    {
        _mapper = mapper;
        _repository = repository;
        _expertService = expertService;
        _customerService = customerService;
        _commentService = commentService;
        _categoryService = categoryService;
        _appSettings = appSettings.Value;
    }

    public async Task<IEnumerable<UploadDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _repository.GetAll(cancellationToken);
    }

    public async Task<UploadDto> GetById(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<int> Remove(int id, string uploadsRootFolder, CancellationToken cancellationToken)
    {
        var record = await _repository.GetById(id, cancellationToken);
        var recordId = await _repository.Remove(id);
        if (File.Exists(Path.Combine(uploadsRootFolder, record.FileName)))
        {
            File.Delete(Path.Combine(uploadsRootFolder, record.FileName));
        }
        else throw new NotFoundException(nameof(record), record.FileName);
        return recordId;
    }

    /// <summary>
    /// gets the uploadDto and uploadRootFolder and saves the file to uploadRootFolder and saves the 
    /// meta information in the repository
    /// </summary>
    /// <param name="dto">Upload Dto containing info such as file name and size and the IFormFile itself</param>
    /// <param name="uploadsRootFolder">The root folder that the file be saved in</param>
    /// <returns></returns>
    public async Task<int> Set(UploadServiceDto dto, string uploadsRootFolder, CancellationToken cancellationToken)
    {
        var file = dto.UploadedFile;
        var fileExtension = Path.GetExtension(file.FileName);

        // make a new file name to make removing it easier
        var newFileName = Guid.NewGuid() + fileExtension;

        await SaveFile(file, uploadsRootFolder, newFileName);



        var uploadId = await _repository.Add(new UploadDto
        {
            FileSize = dto.FileSize,
            FileName = newFileName,
            FileCategory = dto.FileCategory,
        });
        try
        {

            switch (dto.FileCategory)
            {
                case FileCategory.Customer:
                    var customerId = dto.CustomerId;
                    var customer = await _customerService.GetById(customerId!, cancellationToken);
                    // remove old profile pic if it exists
                    if (customer.ProfilePictureId != null)
                    {
                        await Remove((int)customer.ProfilePictureId, uploadsRootFolder, cancellationToken);
                    }
                    customer.ProfilePictureId = uploadId;
                    await _customerService.Update(customer, cancellationToken);
                    break;
                case FileCategory.Comment:
                    var commentId = dto.CommentId;
                    var comment = await _commentService.GetById((int)commentId!, cancellationToken);
                    comment.ImageId = uploadId;
                    await _commentService.Update(comment, cancellationToken);
                    break;
                case FileCategory.ServiceCategory:
                    var categoryId = dto.CategoryId;
                    var category = await _categoryService.GetById((int)categoryId!, cancellationToken);
                    category.PictureId = uploadId;
                    await _categoryService.Update(category, cancellationToken);
                    break;
                case FileCategory.ExpertProfilePic:
                    var expertId = dto.ExpertId;
                    var expert = await _expertService.GetById(expertId!, cancellationToken);
                    // remove old profile pic if it exists
                    if (expert.ProfilePictureId != null)
                    {
                        await Remove((int)expert.ProfilePictureId, uploadsRootFolder, cancellationToken);
                    }
                    expert.ProfilePictureId = uploadId;
                    await _expertService.Update(expert, cancellationToken);
                    break;
                default:
                    break;
            }
        }
        catch (Exception)
        {
            // if there's a problem in other repos
            await Remove(uploadId, uploadsRootFolder, cancellationToken);
            throw;
        }
        return uploadId;

    }

    // TODO : make it get the uploads root folder from config?
    public async Task SaveFile(IFormFile file, string uploadsRootFolder, string newFileName)
    {
        if (!Directory.Exists(uploadsRootFolder))
        {
            Directory.CreateDirectory(uploadsRootFolder);
        }
        // get file path
        var filePath = Path.Combine(uploadsRootFolder, newFileName);
        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<int> SetExpertWorkSamples(UploadServiceDto workSample, string uploadsRootFolder, CancellationToken cancellationToken)
    {
        if (workSample.UploadedFile == null || workSample.UploadedFile.Length == 0)
        {
            throw new NotSupportedException();
        }

        var expertId = workSample.ExpertId;
        var expert = await _expertService.GetById(expertId!, cancellationToken);

        var file = workSample.UploadedFile;
        var fileExtension = Path.GetExtension(file.FileName);
        var newFileName = Guid.NewGuid() + fileExtension;

        await SaveFile(file, uploadsRootFolder, newFileName);



        var uploadId = await _repository.Add(new UploadDto
        {
            ExpertId = expertId,
            FileSize = workSample.FileSize,
            FileName = newFileName,
            FileCategory = FileCategory.ExpertWorkSample,
        });

        return uploadId;
    }

    public async Task<int> Update(UploadDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }

    public async Task<string> GetFileDirectory(int fileId, CancellationToken cancellationToken)
    {
        var file = await _repository.GetById(fileId, cancellationToken);
        return _appSettings.UploadsFolderName + file.FileName;
    }
}