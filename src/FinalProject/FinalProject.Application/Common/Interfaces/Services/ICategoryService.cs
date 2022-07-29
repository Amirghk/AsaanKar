using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface ICategoryService
{
    Task<int> Set(CategoryDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<CategoryDto>> GetAll(CancellationToken cancellationToken);
    Task<CategoryDto> GetById(int id, CancellationToken cancellationToken);
    Task<int> Remove(int id, CancellationToken cancellationToken);
    Task<int> Update(CategoryDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<CategoryDto>> GetChildren(int id, CancellationToken cancellationToken);
}
