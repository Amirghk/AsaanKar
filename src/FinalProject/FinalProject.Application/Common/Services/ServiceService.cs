using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Services;

public class ServiceService : IServiceService
{
    private readonly IMapper _mapper;
    private readonly IServiceRepository _repository;
    public ServiceService(IServiceRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<ServiceDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<ServiceDto> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<int> Remove(int id)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(ServiceDto dto)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(ServiceDto dto)
    {
        return await _repository.Update(dto);
    }
}