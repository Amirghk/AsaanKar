using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

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