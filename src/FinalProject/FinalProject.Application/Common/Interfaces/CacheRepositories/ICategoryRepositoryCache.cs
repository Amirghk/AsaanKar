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
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<CategoryDto> Get(int id);
        Task Delete(int id);
        Task Set(IEnumerable<CategoryDto> categories);
        Task<IEnumerable<CategoryDto>> GetChildren(int id);
    }
}
