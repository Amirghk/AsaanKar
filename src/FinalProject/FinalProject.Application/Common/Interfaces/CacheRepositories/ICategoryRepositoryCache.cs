using FinalProject.Application.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Interfaces.CacheRepositories
{
    public interface ICategoryRepositoryCache
    {
        Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken);
        Task<CategoryDto> Get(int id, CancellationToken cancellationToken);
        Task Set(IEnumerable<CategoryDto> categories);
        Task Set(CategoryDto categoryDto);
        Task Delete(int id);
        Task<IEnumerable<CategoryDto>> GetChildren(int id, CancellationToken cancellationToken);
    }
}
