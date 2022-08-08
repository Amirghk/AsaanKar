using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Application.Common.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IUploadRepository _uploadRepository;
    private readonly ICategoryRepository _repository;
    public CategoryService(ICategoryRepository repository, IMapper mapper, IUploadRepository uploadRepository)
    {
        _mapper = mapper;
        _uploadRepository = uploadRepository;
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _repository.GetAll(cancellationToken);
    }

    public async Task<CategoryDto> GetById(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<IEnumerable<CategoryDto>> GetChildren(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetChildren(id, cancellationToken);
    }

    public async Task<int> Remove(int id, CancellationToken cancellationToken)
    {
        var category = await _repository.GetById(id, cancellationToken);
        if (category.PictureId != null)
        {
            await _uploadRepository.Remove((int)category.PictureId);
        }
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CategoryDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(CategoryDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }
}