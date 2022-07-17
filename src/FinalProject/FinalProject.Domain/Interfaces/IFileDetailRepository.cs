using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IFileDetailRepository
{
    Task<int> Add(Upload model);
    Task<int> Update(Upload model);
    Task<int> Remove(int id);
    Task<Upload> GetById(int id);
    Task<IEnumerable<Upload>> GetAll();
}