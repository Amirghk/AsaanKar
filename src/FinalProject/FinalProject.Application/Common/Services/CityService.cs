using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
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
        List<CityDto> mappedModels = _mapper.Map<List<CityDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<CityDto> GetById(int id)
    {
        CityDto mappedModel = _mapper.Map<CityDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CityDto dto)
    {
        return await _repository.Add(_mapper.Map<City>(dto));
    }

    public async Task<int> Update(CityDto dto)
    {
        return await _repository.Update(_mapper.Map<City>(dto));
    }
}