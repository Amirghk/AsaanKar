using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.Interfaces.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Application.Common.DataTransferObjects;
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

        public async Task<IEnumerable<ServiceDto>> GetAll(CancellationToken cancellationToken, int? categoryId = null, string? expertId = null)
        {
            if (expertId is not null)
            {
                return await _mapper.ProjectTo<ServiceDto>(_context.Services.Where(x => x.Experts.Any(x => x.Id == expertId))).ToListAsync(cancellationToken);
            }
            return await _mapper.ProjectTo<ServiceDto>(_context.Services).Where(x => x.CategoryId == categoryId || true).ToListAsync(cancellationToken);
        }


        public async Task<ServiceDto> GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _mapper.ProjectTo<ServiceDto>(_context.Services).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
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
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
