using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

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
        return await _repository.GetAll();
    }

    public async Task<ProvinceDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(ProvinceDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(ProvinceDto dto)
    {
        return await _repository.Update(dto);
    }
}