using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Interfaces;
public interface IBidRepository
{
    Task<int> Add(BidDto model);
    Task<int> Update(BidDto model);
    Task<int> Remove(int id);
    Task<BidDto> GetById(int id);
    Task<IEnumerable<BidDto>> GetAll();
}

