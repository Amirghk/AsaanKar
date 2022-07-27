using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace FinalProject.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(
            ApplicationDbContext context,
            IMapper mapper,
            ILogger<CategoryRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Add(CategoryDto model)
        {
            _logger.LogTrace("Start of {method}", nameof(Add));
            var record = _mapper.Map<Category>(model);
            await _context.Categories.AddAsync(record);
            await _context.SaveChangesAsync();
            _logger.LogTrace("End of {method}", nameof(Add));
            return record.Id;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            _logger.LogTrace("Start of {method}", nameof(GetAll));
            var records = await _mapper.ProjectTo<CategoryDto>(_context.Categories).ToListAsync();
            _logger.LogTrace("End of {method}", nameof(GetAll));
            return records;
        }

        public async Task<CategoryDto> GetById(int id)
        {
            _logger.LogTrace("Start of {method}", nameof(GetById));
            var record = await _mapper.ProjectTo<CategoryDto>(_context.Categories).SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Category), id);
            }
            _logger.LogTrace("End of {method}", nameof(GetById));
            return record;
        }

        public async Task<IEnumerable<CategoryDto>> GetChildren(int id)
        {
            _logger.LogTrace("Start of {method}", nameof(GetChildren));
            var records = await _mapper.ProjectTo<CategoryDto>(_context.Categories).Where(x => x.ParentCategoryId == id).ToListAsync();
            _logger.LogTrace("End of {method}", nameof(GetChildren));
            return records;
        }

        public async Task<int> Remove(int id)
        {
            _logger.LogTrace("Start of {method}", nameof(Remove));
            var record = await _context.Categories.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Category), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            _logger.LogTrace("End of {method}", nameof(Remove));
            return id;
        }

        public async Task<int> Update(CategoryDto model)
        {
            _logger.LogTrace("Start of {method}", nameof(Update));
            var record = await _context.Categories.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Category), model.Id);
            }
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            _logger.LogTrace("End of {method}", nameof(Update));
            return record.Id;
        }
    }
}
