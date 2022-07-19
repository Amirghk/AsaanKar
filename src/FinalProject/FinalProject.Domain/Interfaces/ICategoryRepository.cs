using System.Linq.Expressions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<int> Add(CategoryDto model);
    Task<int> Update(CategoryDto model);
    Task<int> Remove(int id);
    Task<CategoryDto> GetById(int id);
    Task<IEnumerable<CategoryDto>> GetAll();
}