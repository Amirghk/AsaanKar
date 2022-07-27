using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Application.Common.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _repository;
    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<CategoryDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<IEnumerable<CategoryDto>> GetChildren(int id)
    {
        return await _repository.GetChildren(id);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CategoryDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(CategoryDto dto)
    {
        return await _repository.Update(dto);
    }
}