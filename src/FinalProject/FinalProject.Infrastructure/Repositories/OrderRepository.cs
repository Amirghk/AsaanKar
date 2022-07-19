using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
            var newRecord = _mapper.Map<Order>(model);
            record = newRecord;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
