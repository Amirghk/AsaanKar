using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace FinalProject.Application.Common.Services;

public class
    CommentService : ICommentService
{
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;
    private readonly ILogger<CommentService> _logger;
    private readonly ICommentRepository _repository;
    public CommentService(ICommentRepository repository, IMapper mapper, IOrderService orderService, ILogger<CommentService> logger)
    {
        _mapper = mapper;
        _orderService = orderService;
        _logger = logger;
        _repository = repository;
    }

    public async Task<int> Approve(int id, CancellationToken cancellationToken)
    {
        var record = await _repository.GetById(id, cancellationToken);
        record.IsApproved = true;
        await _repository.Update(record);
        return record.Id;

    }

    public async Task<IEnumerable<CommentDto>> GetAll(CancellationToken cancellationToken)
    {
        return await _repository.GetAll(cancellationToken);
    }

    public async Task<CommentDto> GetById(int id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<IEnumerable<CommentDto>> GetByUserId(string id, CancellationToken cancellationToken)
    {
        return await _repository.GetByUserId(id, cancellationToken);
    }

    public async Task<int> Remove(int id, CancellationToken cancellationToken)
    {
        return await _repository.Remove(id);
    }

    public async Task<int> Set(CommentDto dto, CancellationToken cancellationToken)
    {
        var customerOrders = await _orderService.GetByUserId(dto.CustomerId, cancellationToken);
        var expertOrders = await _orderService.GetByUserId(dto.ExpertId, cancellationToken);
        if (!customerOrders.Select(x => x.Id).Intersect(expertOrders.Select(x => x.Id)).Any())
        {
            _logger.LogError("UNAUTHORIZED : user {user} tried to comment on {expert}", dto.CustomerId, dto.ExpertId);
            throw new InvalidOperationException("User doesn't have permission to comment on expert!");
        }
        return await _repository.Add(dto);
    }

    public async Task<int> Update(CommentDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Update(dto);
    }
}