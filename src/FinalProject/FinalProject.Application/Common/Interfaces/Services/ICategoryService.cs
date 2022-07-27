using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface ICategoryService
{
    Task<int> Set(CategoryDto dto);
    Task<IEnumerable<CategoryDto>> GetAll();
    Task<CategoryDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(CategoryDto dto);
    Task<IEnumerable<CategoryDto>> GetChildren(int id);
}
