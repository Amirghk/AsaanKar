using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Infrastructure.Repositories
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProvinceRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(ProvinceDto model)
        {
            var record = _mapper.Map<Province>(model);
            await _context.Provinces.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<ProvinceDto>> GetAll()
        {
            return await _mapper.ProjectTo<ProvinceDto>(_context.Provinces).ToListAsync();
        }

        public async Task<ProvinceDto> GetById(int id)
        {
            var record = await _mapper.ProjectTo<ProvinceDto>(_context.Provinces).SingleOrDefaultAsync(x => x.Id == id);
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

        public async Task<int> Update(ProvinceDto model)
        {
            var record = await _context.Provinces.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Province), model.Id);
            }
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
