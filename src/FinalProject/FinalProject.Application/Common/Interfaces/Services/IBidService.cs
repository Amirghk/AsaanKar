using FinalProject.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IBidService
{
    Task<int> Set(BidDto dto);
    Task<IEnumerable<BidDto>> GetAll();
    Task<BidDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(BidDto dto);
}

