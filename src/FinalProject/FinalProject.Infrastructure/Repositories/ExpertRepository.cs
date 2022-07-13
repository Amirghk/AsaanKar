using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Infrastructure.Repositories
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpertRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Expert model)
        {
            await _context.Experts.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<Expert>> GetAll()
        {
            return await _context.Experts.ToListAsync();
        }

        public async Task<Expert> GetById(int id)
        {
            var record = await _context.Experts.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), id);
            }
            return record;
        }

        public async Task<int> Remove(int id)
        {
            var record = await _context.Experts.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(Expert model)
        {
            var record = await _context.Experts.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), model.Id);
            }
            record = model;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
