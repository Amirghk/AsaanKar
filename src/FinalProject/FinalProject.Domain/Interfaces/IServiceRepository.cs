using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IServiceRepository
{
    Task<int> Add(Service model);
    Task<int> Update(Service model);
    Task<int> Remove(int id);
    Task<Service> GetById(int id);
    Task<IEnumerable<Service>> GetAll();
}