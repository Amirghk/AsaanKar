using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Category model)
        {
            await _context.Services.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Services.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            var record = await _context.Services.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Category), id);
            }
            return record;
        }

        public async Task<int> Remove(int id)
        {
            var record = await _context.Services.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Category), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(Category model)
        {
            var record = await _context.Services.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Category), model.Id);
            }
            record = model;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
