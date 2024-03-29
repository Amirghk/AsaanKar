using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Application.Common.Services;

public class ServiceService : IServiceService
{
    private readonly IMapper _mapper;
    private readonly IServiceRepository _repository;
    public ServiceService(
        IServiceRepository repository,
        IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<ServiceDto>> GetAll(CancellationToken cancellationToken, int? categoryId = null, string? expertId = null)
    {
        var records = await _repository.GetAll(cancellationToken, categoryId, expertId);
        return records.Where(x => x.IsDeleted is false);
    }


    public async Task<ServiceDto> GetById(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<int> Remove(int id, CancellationToken cancellationToken)
    {
        return await _repository.SoftDelete(id);
    }

    public async Task<int> Set(ServiceDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(ServiceDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }
}