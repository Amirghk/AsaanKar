using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(OrderDto model)
        {
            var record = _mapper.Map<Order>(model);
            await _context.Orders.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            return await _mapper.ProjectTo<OrderDto>(_context.Orders).ToListAsync();
        }

        public async Task<OrderDto> GetById(int id)
        {
            var record = await _mapper.ProjectTo<OrderDto>(_context.Orders).SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Order), id);
            }
            return record;
        }


        public async Task<int> Remove(int id)
        {
            var record = await _context.Orders.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Order), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(OrderDto model)
        {
            var record = await _context.Orders.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Order), model.Id);
            }
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
