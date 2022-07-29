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
    private readonly IExpertRepository _repository;
    public ExpertService(IExpertRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
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