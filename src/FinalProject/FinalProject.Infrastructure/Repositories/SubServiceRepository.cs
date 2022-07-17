using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Infrastructure.Repositories
{
    public class SubServiceRepository : ISubServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public SubServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Service model)
        {
            await _context.SubServices.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            return await _context.SubServices.AsNoTracking().ToListAsync();
        }

        public async Task<Service> GetById(int id)
        {
            var record = await _context.SubServices.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Service), id);
            }
            return record;
        }

        public async Task<int> Remove(int id)
        {
            var record = await _context.SubServices.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Service), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(Service model)
        {
            var record = await _context.SubServices.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Service), model.Id);
            }
            record = model;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
