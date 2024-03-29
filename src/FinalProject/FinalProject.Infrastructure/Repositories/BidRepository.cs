﻿using AutoMapper;
using FinalProject.Application.Common.Exceptions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinalProject.Application.Common.Interfaces.Repositories;

namespace FinalProject.Infrastructure.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BidRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(BidDto model)
        {
            var record = _mapper.Map<Bid>(model);
            await _context.Bids.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task<IEnumerable<BidDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<BidDto>(_context.Bids).ToListAsync();
        }

        public async Task<BidDto> GetById(int id, CancellationToken cancellationToken)
        {
            var record = await _mapper.ProjectTo<BidDto>(_context.Bids).SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Bid), id);
            }
            return record;
        }


        public async Task<int> Remove(int id)
        {
            var record = await _context.Bids.FindAsync(id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Bid), id);
            }
            _context.Remove(record);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> Update(BidDto model)
        {
            var record = await _context.Bids.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(Bid), model.Id);
            }
            _mapper.Map(model, record);
            await _context.SaveChangesAsync();
            return record.Id;
        }
    }
}
