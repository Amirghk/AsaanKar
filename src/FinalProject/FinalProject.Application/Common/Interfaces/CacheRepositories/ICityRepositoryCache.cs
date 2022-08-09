using FinalProject.Application.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Interfaces.CacheRepositories
{
    public interface ICityRepositoryCache
    {
        Task<IEnumerable<CityDto>> GetAll(CancellationToken cancellationToken);
        Task<CityDto> Get(int id, CancellationToken cancellationToken);
        Task Set(IEnumerable<CityDto> categories);
    }
}
