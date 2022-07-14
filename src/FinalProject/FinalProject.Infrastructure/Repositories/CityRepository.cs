using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(City model)
        {
            await _context.Cities.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _context.Cities.AsNoTracking().ToListAsync();
        }

        public async Task<City> GetById(int id)
        {
            var record = await _context.Cities.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(City), id);
            }
            return record;
        }

        public async Task<int> Remove(int id)
        {
            var record = await _context.Cities.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(City), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(City model)
        {
            var record = await _context.Cities.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(City), model.Id);
            }
            record = model;
            await _context.SaveChangesAsync();
            return record.Id;
        }

    }
}
