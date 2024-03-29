using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;
using FinalProject.Domain.Enums;

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

        public async Task<IEnumerable<UploadDto>> GetAll(CancellationToken cancellationToken, string? expertId = null, FileCategory? fileCategory = null)
        {
            IQueryable<Upload> query = _context.Uploads;

            if (expertId is not null)
            {
                query = query.Where(x => x.ExpertId == expertId);
            }
            if (fileCategory is not null)
            {
                query.Where(x => x.FileCategory == fileCategory);
            }

            return await _mapper.ProjectTo<UploadDto>(query).ToListAsync(cancellationToken);
        }

        public async Task<UploadDto> GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _mapper.ProjectTo<UploadDto>(_context.Uploads).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
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
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
