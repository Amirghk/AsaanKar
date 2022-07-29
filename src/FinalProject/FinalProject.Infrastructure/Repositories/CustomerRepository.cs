using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;

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

        public async Task<string> Add(CustomerDto model)
        {
            var record = _mapper.Map<Customer>(model);
            await _context.Customers.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id!;
        }

        public async Task<IEnumerable<CustomerDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<CustomerDto>(_context.Customers).ToListAsync(cancellationToken);
        }

        public async Task<CustomerDto> GetById(string id, CancellationToken cancellationToken)
        {
            var record = await _mapper.ProjectTo<CustomerDto>(_context.Customers).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (record == null)
            {
                throw new NotFoundException(nameof(Customer), id);
            }
            return record;
        }

        public async Task<string> Remove(string id)
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

        public async Task<string> SoftDelete(string customerId)
        {
            var record = await _context.Customers.SingleOrDefaultAsync(x => x.Id == customerId);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), customerId);
            }
            record.IsDeleted = true;
            await _context.SaveChangesAsync();
            return record.Id!;
        }

        public async Task<string> Update(CustomerDto model)
        {
            var record = await _context.Customers.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Customer), model.Id);
            }
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id!;
        }
    }
}
