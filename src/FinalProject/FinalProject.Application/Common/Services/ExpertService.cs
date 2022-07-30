using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Application.Common.Services;

public class ExpertService : IExpertService
{
    private readonly IMapper _mapper;
    private readonly IServiceService _serviceService;
    private readonly IExpertRepository _repository;
    public ExpertService(
        IExpertRepository repository,
        IMapper mapper,
        IServiceService serviceService)
    {
        _mapper = mapper;
        _serviceService = serviceService;
        _repository = repository;
    }

    public async Task<string> AddServices(string expertId, List<int> serviceIds, CancellationToken cancellationToken)
    {
        var services = await _serviceService.GetAll(cancellationToken);
        var addedServices = services.Where(x => serviceIds.Contains(x.Id)).ToList();
        var expert = await _repository.GetById(expertId, cancellationToken);
        var newList = expert.Services.Concat(addedServices).ToList();
        expert.Services = newList;
        return await _repository.Update(expert);
    }

    public async Task<IEnumerable<ExpertDto>> GetAll(CancellationToken cancellationToken)
    {
        return (await _repository.GetAll(cancellationToken)).Where(x => x.IsDeleted == false);
    }

    public async Task<ExpertDto> GetById(string id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<string> GetName(string id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(id, cancellationToken);
        return user.FirstName + " " + user.LastName;
    }

    public async Task<string> Remove(string id, CancellationToken cancellationToken)
    {
        return await _repository.Remove(id);
    }
    /// <summary>
    /// gets expert id and service id and removes the service from the expert and returns id of expert
    /// , throws and exception if expert doesn't have the selected service
    /// </summary>
    /// <param name="expertId">Id of expert</param>
    /// <param name="serviceId">Id of service</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<string> RemoveService(string expertId, int serviceId, CancellationToken cancellationToken)
    {
        var expert = await _repository.GetById(expertId, cancellationToken);
        var service = await _serviceService.GetById(serviceId, cancellationToken);
        var result = expert.Services.Remove(service);
        if (result == false)
        {
            throw new InvalidOperationException("Expert Doesn't have selected service!");
        }
        return await _repository.Update(expert);
    }

    public async Task<string> Set(ExpertDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Add(dto);
    }

    public async Task<string> SoftDelete(string id, CancellationToken cancellationToken)
    {
        return await _repository.SoftDelete(id);
    }

    public async Task<string> Update(ExpertDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }
}