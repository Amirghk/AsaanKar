using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Repositories
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExpertRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(ExpertDto model)
        {
            var record = _mapper.Map<Expert>(model);
            await _context.Experts.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<ExpertDto>> GetAll()
        {
            return await _mapper.ProjectTo<ExpertDto>(_context.Experts).ToListAsync();
        }

        public async Task<ExpertDto> GetById(int id)
        {
            var record = await _mapper.ProjectTo<ExpertDto>(_context.Experts).SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), id);
            }
            return record;
        }

        public async Task<ExpertDto> GetByUserId(string userId)
        {
            var record = await _mapper.ProjectTo<ExpertDto>(_context.Experts).SingleOrDefaultAsync(x => x.ExpertId == userId);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), userId);
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

        public async Task<int> Update(ExpertDto model)
        {
            var record = await _context.Experts.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Expert), model.Id);
            }
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
