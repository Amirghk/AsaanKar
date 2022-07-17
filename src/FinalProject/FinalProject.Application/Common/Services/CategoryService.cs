using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

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
        List<CategoryDto> mappedModels = _mapper.Map<List<CategoryDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<Dtos.CategoryDto> GetById(int id)
    {
        CategoryDto mappedModel = _mapper.Map<CategoryDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CategoryDto dto)
    {
        return await _repository.Add(_mapper.Map<Category>(dto));
    }

    public async Task<int> Update(CategoryDto dto)
    {
        return await _repository.Update(_mapper.Map<Category>(dto));
    }
}