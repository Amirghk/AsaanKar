using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Repositories;
public class BidRepository : IBidRepository
{
    private readonly ApplicationDbContext _context;

    public BidRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Add(Bid model)
    {
        await _context.Bids.AddAsync(model);
        await _context.SaveChangesAsync();
        return model.Id;
    }

    public async Task<IEnumerable<Bid>> GetAll()
    {
        return await _context.Bids.AsNoTracking().ToListAsync();
    }

    public async Task<Bid> GetById(int id)
    {
        var record = await _context.Bids.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
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

    public async Task<int> Update(Bid model)
    {
        var record = await _context.Bids.SingleOrDefaultAsync(x => x.Id == model.Id);
        if (record == null)
        {
            throw new NotFoundException(nameof(Bid), model.Id);
        }
        record = model;
        await _context.SaveChangesAsync();
        return record.Id;
    }
}
