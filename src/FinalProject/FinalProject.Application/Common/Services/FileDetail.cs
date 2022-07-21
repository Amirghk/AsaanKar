using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class UploadService : IUploadService
{
    private readonly IMapper _mapper;
    private readonly IUploadRepository _repository;
    private readonly IExpertService _expertService;
    public UploadService(IUploadRepository repository, IMapper mapper, IExpertService expertService)
    {
        _mapper = mapper;
        _repository = repository;
        _expertService = expertService;
    }

    public async Task<IEnumerable<UploadDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<UploadDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<int> Remove(int id, string uploadsRootFolder)
    {
        var record = await _repository.GetById(id);
        if (File.Exists(Path.Combine(uploadsRootFolder, record.FileName)))
        {
            File.Delete(Path.Combine(uploadsRootFolder, record.FileName));
        }
        else throw new NotFoundException(nameof(record), record.FileName);
        return await _repository.Remove(id);
    }

    /// <summary>
    /// gets the uploadDto and uploadRootFolder and saves the file to uploadRootFolder and saves the 
    /// meta information in the repository
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="uploadsRootFolder"></param>
    /// <returns></returns>
    public async Task<int> Set(UploadServiceDto dto, string uploadsRootFolder)
    {
        if (!Directory.Exists(uploadsRootFolder))
        {
            Directory.CreateDirectory(uploadsRootFolder);
        }
        var file = dto.UploadedFile;
        var fileExtension = Path.GetExtension(file.FileName);

        // make a new file name to make removing it easier
        var newFileName = Guid.NewGuid() + fileExtension;
        // get file path
        var filePath = Path.Combine(uploadsRootFolder, newFileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
            //.ConfigureAwait(false);
        }
        // see what category file belongs to
        var uploadId = await _repository.Add(new UploadDto
        {
            ExpertId = dto.ExpertId,
            FileSize = dto.FileSize,
            FileName = newFileName,
            FileCategory = dto.FileCategory,
        });
        // TODO ---------------------------
        switch (dto.FileCategory)
        {
            case FileCategory.Customer:
                break;
            case FileCategory.Expert:
                break;
            case FileCategory.Comment:
                break;
            case FileCategory.Service:
                break;
            case FileCategory.ExpertProfilePic:
                var expertId = dto.ExpertId;
                var expert = await _expertService.GetById((int)expertId!);
                expert.ProfilePictureId = uploadId;
                await _expertService.Update(expert);
                break;
            default:
                break;
        }
        return uploadId;

    }

    public async Task<int> Update(UploadDto dto)
    {
        return await _repository.Update(dto);
    }
}