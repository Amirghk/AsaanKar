using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using FinalProject.Domain.Enums;

namespace FinalProject.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(ApplicationDbContext context, IMapper mapper, ILogger<OrderRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Add(OrderDto model)
        {
            var record = _mapper.Map<Order>(model);
            await _context.Orders.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<OrderDto>> GetAll(CancellationToken cancellationToken, int? cityId = null, string? userId = null, OrderState? fromState = null, OrderState? toState = null)
        {
            _logger.LogTrace("start of {methodName}", nameof(GetAll));
            IQueryable<Order> query = _context.Orders.Include(x => x.Bids);
            if (cityId != null)
                query = query.Where(x => x.ReceiverAddress.CityId == cityId);
            if (fromState is not null && toState is not null)
            {
                var states = Enumerable.Range((int)fromState, (int)toState - (int)fromState + 1);
                // convert int of enums to values
                var stateEnums = states.Select(x => (OrderState)x);
                query = query.Where(x => stateEnums.Contains(x.State));
            }
            if (fromState is not null && toState is null)
            {
                query = query.Where(x => x.State == (OrderState)fromState);
            }
            if (userId != null)
                query = query.Where(x => x.CustomerId == userId
                            || x.ExpertId == userId);

            var records = await _mapper.ProjectTo<OrderDto>(query).ToListAsync(cancellationToken);
            return records;
        }


        public async Task<OrderDto> GetById(int id, CancellationToken cancellationToken)
        {
            _logger.LogTrace("start of {}", nameof(GetById));
            // TODO
            var record = await _mapper.ProjectTo<OrderDto>(_context.Orders).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (record == null)
            {
                _logger.LogError("Failed to find Order with id : {id}", id);
                throw new NotFoundException(nameof(Order), id);
            }
            _logger.LogTrace("returning order with id : {id}", id);
            return record;
        }


        public async Task<int> Remove(int id)
        {
            _logger.LogTrace("start of {}", nameof(Remove));
            var record = await _context.Orders.FindAsync(id);
            if (record == null)
            {
                _logger.LogError("Failed to find Order with id : {}", id);
                throw new NotFoundException(nameof(Order), id);
            }
            _logger.LogTrace("removing order with id : {}", id);
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(OrderDto model)
        {
            _logger.LogTrace("start of {}", nameof(Update));
            var record = await _context.Orders.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                _logger.LogError("Failed to find Order with id : {}", model.Id);
                throw new NotFoundException(nameof(Order), model.Id);
            }
            _mapper.Map(model, record);
            _logger.LogTrace("updating order with id : {}", model.Id);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
