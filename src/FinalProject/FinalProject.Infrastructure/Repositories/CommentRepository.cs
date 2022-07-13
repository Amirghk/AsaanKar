using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Comment model)
        {
            await _context.Comments.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetById(int id)
        {
            var record = await _context.Comments.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Comment), id);
            }
            return record;
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

        public async Task<int> Update(Comment model)
        {
            var record = await _context.Comments.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Comment), model.Id);
            }
            record = model;
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
