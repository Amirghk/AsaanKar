namespace FinalProject.Application.Common.Interfaces.Persistence;

public interface IRepository<T>
{
    int Add(T dto);
    int Update(T dto);
    int Remove(T dto);
    T GetById(int id);
    IQueryable<T> GetAll();
}