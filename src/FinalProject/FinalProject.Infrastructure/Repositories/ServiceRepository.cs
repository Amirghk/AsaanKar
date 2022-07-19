using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ServiceRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(ServiceDto model)
        {
            var record = _mapper.Map<Service>(model);
            await _context.Services.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<ServiceDto>> GetAll()
        {
            return await _mapper.ProjectTo<ServiceDto>(_context.Services).ToListAsync();
        }

        public async Task<ServiceDto> GetById(int id)
        {
            var record = await _mapper.ProjectTo<ServiceDto>(_context.Services).SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Service), id);
            }
            return record;
        }


        public async Task<int> Remove(int id)
        {
            var record = await _context.Services.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Service), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(ServiceDto model)
        {
            var record = await _context.Services.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Service), model.Id);
            }
            var newRecord = _mapper.Map<Service>(model);
            record = newRecord;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
