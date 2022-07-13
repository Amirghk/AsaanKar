using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface ICityRepository
{
    Task<int> Add(City model);
    Task<int> Update(City model);
    Task<int> Remove(int id);
    Task<City> GetById(int id);
    Task<IEnumerable<City>> GetAll();
}