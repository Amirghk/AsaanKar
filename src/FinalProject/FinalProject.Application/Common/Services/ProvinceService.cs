using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class ProvinceService : IProvinceService
{
    private readonly IMapper _mapper;
    private readonly IProvinceRepository _repository;
    public ProvinceService(IProvinceRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<ProvinceDto>> GetAll()
    {
        List<ProvinceDto> mappedModels = _mapper.Map<List<ProvinceDto>>(await _repository.GetAll()).ToList();
        return mappedModels;
    }

    public async Task<ProvinceDto> GetById(int id)
    {
        ProvinceDto mappedModel = _mapper.Map<ProvinceDto>(await _repository.GetById(id));
        return mappedModel;
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(ProvinceDto dto)
    {
        return await _repository.Add(_mapper.Map<Province>(dto));
    }

    public async Task<int> Update(ProvinceDto dto)
    {
        return await _repository.Update(_mapper.Map<Province>(dto));
    }
}