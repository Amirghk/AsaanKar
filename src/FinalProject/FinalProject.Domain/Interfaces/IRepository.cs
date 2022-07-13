using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;
// https://programmingwithmosh.com/net/common-mistakes-with-the-repository-pattern/
public interface IRepository<T> where T : IBaseEntity
{
    Task<int> Add(T model);
    Task<int> Update(T model);
    Task<int> Remove(int id);
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    // IEnumerable<T> Find(Expression<Func<T, bool>> query);
}