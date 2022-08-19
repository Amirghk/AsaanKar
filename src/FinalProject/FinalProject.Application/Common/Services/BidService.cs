using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;
using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Services;

public class BidService : IBidService
{
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;
    private readonly IBidRepository _repository;
    public BidService(
        IBidRepository repository,
        IMapper mapper,
        IOrderService orderService)
    {
        _mapper = mapper;
        _orderService = orderService;
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
        // TODO check status
        return await _repository.Remove(id);
    }

    public async Task<int> Set(BidDto dto, CancellationToken cancellationToken)
    {
        var bidId = await _repository.Add(dto);
        var order = await _orderService.GetById(dto.OrderId, cancellationToken);
        // change order state
        if (order.State == OrderState.WaitingForExpertBid)
        {
            await _orderService.ExpertBidAdded(dto.OrderId, cancellationToken);
        }
        return bidId;
    }

    public async Task<int> Update(BidDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }
}