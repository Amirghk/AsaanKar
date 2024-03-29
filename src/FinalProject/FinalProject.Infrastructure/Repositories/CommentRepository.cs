using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CommentRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(CommentDto model)
        {
            var record = _mapper.Map<Comment>(model);
            await _context.Comments.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<CommentDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<CommentDto>(_context.Comments).ToListAsync(cancellationToken);
        }

        public async Task<CommentDto> GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _mapper.ProjectTo<CommentDto>(_context.Comments).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (record == null)
            {
                throw new NotFoundException(nameof(Comment), id);
            }
            return record;
        }

        public async Task<IEnumerable<CommentDto>> GetByUserId(string id, CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<CommentDto>(_context.Comments.Where(x => x.CustomerId == id || x.ExpertId == id)).ToListAsync(cancellationToken);
        }

        public async Task<int> Remove(int id)
        {
            var record = await _context.Comments.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Comment), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(CommentDto model)
        {
            var record = await _context.Comments.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Comment), model.Id);
            }
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
