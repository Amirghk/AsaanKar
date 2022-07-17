using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IServiceRepository
{
    Task<int> Add(Category model);
    Task<int> Update(Category model);
    Task<int> Remove(int id);
    Task<Category> GetById(int id);
    Task<IEnumerable<Category>> GetAll();
}