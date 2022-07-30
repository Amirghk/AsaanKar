using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Application.Common.Services;

public class BidService : IBidService
{
    private readonly IMapper _mapper;
    private readonly IBidRepository _repository;
    public BidService(IBidRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<BidDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _repository.GetAll(cancellationToken);
    }

    public async Task<BidDto> GetById(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<int> Remove(int id, CancellationToken cancellationToken)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(BidDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Add(dto);
    }

    public async Task<int> Update(BidDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }
}