using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IProvinceRepository
{
    Task<int> Add(Province model);
    Task<int> Update(Province model);
    Task<int> Remove(int id);
    Task<Province> GetById(int id);
    Task<IEnumerable<Province>> GetAll();
}