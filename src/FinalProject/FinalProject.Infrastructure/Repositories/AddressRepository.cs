using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddressRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(AddressDto model)
        {
            var record = _mapper.Map<Address>(model);
            await _context.Addresses.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<AddressDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<AddressDto>(_context.Addresses).ToListAsync(cancellationToken);
        }

        public async Task<AddressDto> GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _mapper.ProjectTo<AddressDto>(_context.Addresses).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (record == null)
            {
                throw new NotFoundException(nameof(Address), id);
            }
            return record;
        }

        public async Task<IEnumerable<AddressDto>> GetByUserId(string userId, CancellationToken cancellationToken)
        {
            var record = await _mapper.ProjectTo<AddressDto>(_context.Addresses).Where(x => x.ExpertId == userId || x.CustomerId == userId).ToListAsync(cancellationToken);
            if (record == null)
            {
                throw new NotFoundException(nameof(Address), userId);
            }
            return record;
        }

        public async Task<int> Remove(int id)
        {
            var record = await _context.Addresses.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Address), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(AddressDto model)
        {
            var record = await _context.Addresses.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Address), model.Id);
            }
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
