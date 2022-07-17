using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Infrastructure.Repositories
{
    public class FileDetailRepository : IFileDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public FileDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Upload model)
        {
            await _context.FileDetails.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<Upload>> GetAll()
        {
            return await _context.FileDetails.AsNoTracking().ToListAsync();
        }

        public async Task<Upload> GetById(int id)
        {
            var record = await _context.FileDetails.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Upload), id);
            }
            return record;
        }

        public async Task<int> Remove(int id)
        {
            var record = await _context.FileDetails.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Upload), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(Upload model)
        {
            var record = await _context.FileDetails.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Upload), model.Id);
            }
            record = model;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
