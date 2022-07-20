using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CityRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(CityDto model)
        {
            var record = _mapper.Map<City>(model);
            await _context.Cities.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<CityDto>> GetAll()
        {
            return await _mapper.ProjectTo<CityDto>(_context.Cities).ToListAsync();
        }

        public async Task<CityDto> GetById(int id)
        {
            var record = await _mapper.ProjectTo<CityDto>(_context.Cities).SingleOrDefaultAsync(x => x.Id == id);
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

        public async Task<int> Update(CityDto model)
        {
            var record = await _context.Cities.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(City), model.Id);
            }
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
