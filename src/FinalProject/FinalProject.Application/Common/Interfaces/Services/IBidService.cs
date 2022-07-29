using FinalProject.Application.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface IBidService
{
    Task<int> Set(BidDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<BidDto>> GetAll(CancellationToken cancellationToken);
    Task<BidDto> GetById(int id, CancellationToken cancellationToken);
    Task<int> Remove(int id, CancellationToken cancellationToken);
    Task<int> Update(BidDto dto, CancellationToken cancellationToken);
}

