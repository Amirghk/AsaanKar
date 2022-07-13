using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface IFileDetailRepository
{
    Task<int> Add(FileDetail model);
    Task<int> Update(FileDetail model);
    Task<int> Remove(int id);
    Task<FileDetail> GetById(int id);
    Task<IEnumerable<FileDetail>> GetAll();
}