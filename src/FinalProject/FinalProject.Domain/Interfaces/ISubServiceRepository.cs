using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface ISubServiceRepository
{
    Task<int> Add(SubService model);
    Task<int> Update(SubService model);
    Task<int> Remove(int id);
    Task<SubService> GetById(int id);
    Task<IEnumerable<SubService>> GetAll();
}