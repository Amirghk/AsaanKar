using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Infrastructure.Repositories
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly ApplicationDbContext _context;

        public ProvinceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Province model)
        {
            await _context.Provinces.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<Province>> GetAll()
        {
            return await _context.Provinces.ToListAsync();
        }

        public async Task<Province> GetById(int id)
        {
            var record = await _context.Provinces.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Province), id);
            }
            return record;
        }

        public async Task<int> Remove(int id)
        {
            var record = await _context.Provinces.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Province), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(Province model)
        {
            var record = await _context.Provinces.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Province), model.Id);
            }
            record = model;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
