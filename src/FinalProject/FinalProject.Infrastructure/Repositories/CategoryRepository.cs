using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(CategoryDto model)
        {
            var record = _mapper.Map<Category>(model);
            await _context.Categories.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return await _mapper.ProjectTo<CategoryDto>(_context.Categories).ToListAsync();
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var record = await _mapper.ProjectTo<CategoryDto>(_context.Categories).SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Category), id);
            }
            return record;
        }


        public async Task<int> Remove(int id)
        {
            var record = await _context.Categories.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Category), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(CategoryDto model)
        {
            var record = await _context.Categories.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Category), model.Id);
            }
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
