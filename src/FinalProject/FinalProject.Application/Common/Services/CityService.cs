using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class CityService : ICityService
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _repository;
    public CityService(ICityRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<CityDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<CityDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CityDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(CityDto dto)
    {
        return await _repository.Update(dto);
    }
}