using System.Linq.Expressions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<int> Add(CategoryDto model);
    Task<int> Update(CategoryDto model);
    Task<int> Remove(int id);
    Task<CategoryDto> GetById(int id, CancellationToken cancellationToken);
    Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<CategoryDto>> GetChildren(int id, CancellationToken cancellationToken);

}