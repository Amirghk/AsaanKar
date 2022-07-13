using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Customer model)
        {
            await _context.Customers.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            var record = await _context.Customers.FindAsync(id);
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

        public async Task<int> Update(Customer model)
        {
            var record = await _context.Customers.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Customer), model.Id);
            }
            record = model;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
