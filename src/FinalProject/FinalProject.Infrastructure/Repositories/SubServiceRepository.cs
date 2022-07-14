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

        public async Task<int> Add(SubService model)
        {
            await _context.SubServices.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<SubService>> GetAll()
        {
            return await _context.SubServices.AsNoTracking().ToListAsync();
        }

        public async Task<SubService> GetById(int id)
        {
            var record = await _context.SubServices.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(SubService), id);
            }
            return record;
        }

        public async Task<int> Remove(int id)
        {
            var record = await _context.SubServices.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(SubService), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(SubService model)
        {
            var record = await _context.SubServices.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(SubService), model.Id);
            }
            record = model;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
