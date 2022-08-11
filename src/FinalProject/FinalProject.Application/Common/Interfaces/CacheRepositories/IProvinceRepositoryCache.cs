using FinalProject.Application.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Interfaces.CacheRepositories
{
    public interface IProvinceRepositoryCache
    {
        Task<IEnumerable<ProvinceDto>> GetAll(CancellationToken cancellationToken);
        Task<ProvinceDto> Get(int id, CancellationToken cancellationToken);
        Task Set(IEnumerable<ProvinceDto> provinces);
        Task Set(ProvinceDto provinceDto);
        Task Delete(int id);
    }
}
