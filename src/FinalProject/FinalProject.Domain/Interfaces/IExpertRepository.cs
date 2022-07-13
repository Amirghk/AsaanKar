using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IExpertRepository
{
    Task<int> Add(Expert model);
    Task<int> Update(Expert model);
    Task<int> Remove(int id);
    Task<Expert> GetById(int id);
    Task<IEnumerable<Expert>> GetAll();
}