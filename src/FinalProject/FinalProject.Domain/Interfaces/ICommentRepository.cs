using System.Linq.Expressions;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface ICommentRepository
{
    Task<int> Add(Comment model);
    Task<int> Update(Comment model);
    Task<int> Remove(int id);
    Task<Comment> GetById(int id);
    Task<IEnumerable<Comment>> GetAll();
}