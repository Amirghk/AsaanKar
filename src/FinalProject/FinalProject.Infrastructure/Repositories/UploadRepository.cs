using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Repositories
{
    public class UploadRepository : IUploadRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UploadRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(UploadDto model)
        {
            var record = _mapper.Map<Upload>(model);
            await _context.Uploads.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<UploadDto>> GetAll()
        {
            return await _mapper.ProjectTo<UploadDto>(_context.Uploads).ToListAsync();
        }

        public async Task<UploadDto> GetById(int id)
        {
            var record = await _mapper.ProjectTo<UploadDto>(_context.Uploads).SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Upload), id);
            }
            return record;
        }


        public async Task<int> Remove(int id)
        {
            var record = await _context.Uploads.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Upload), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(UploadDto model)
        {
            var record = await _context.Uploads.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Upload), model.Id);
            }
            var newRecord = _mapper.Map<Upload>(model);
            record = newRecord;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
