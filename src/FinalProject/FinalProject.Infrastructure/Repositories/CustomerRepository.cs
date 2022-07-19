using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(CustomerDto model)
        {
            var record = _mapper.Map<Customer>(model);
            await _context.Customers.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            return await _mapper.ProjectTo<CustomerDto>(_context.Customers).ToListAsync();
        }

        public async Task<CustomerDto> GetById(int id)
        {
            var record = await _mapper.ProjectTo<CustomerDto>(_context.Customers).SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Customer), id);
            }
            return record;
        }


        public async Task<int> Remove(int id)
        {
            var record = await _context.Customers.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Customer), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(CustomerDto model)
        {
            var record = await _context.Customers.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Customer), model.Id);
            }
            var newRecord = _mapper.Map<Customer>(model);
            record = newRecord;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
