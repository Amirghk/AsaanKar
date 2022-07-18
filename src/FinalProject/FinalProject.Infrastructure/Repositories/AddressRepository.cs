using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Address model)
        {
            await _context.Addresses.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _context.Addresses.AsNoTracking().ToListAsync();
        }

        public async Task<Address> GetById(int id)
        {
            var record = await _context.Addresses.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Address), id);
            }
            return record;
        }

        public async Task<IEnumerable<Address>> GetByUserId(int userId)
        {
            return await _context.Addresses.Where(x => x.ExpertId == userId || x.CustomerId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<int> Remove(int id)
        {
            var record = await _context.Addresses.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Address), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(Address model)
        {
            // SingleOrDefault tracks related entities?
            var record = await _context.Addresses.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Address), model.Id);
            }
            record = model;
            await _context.SaveChangesAsync();
            return record.Id;
        }

    }
}
