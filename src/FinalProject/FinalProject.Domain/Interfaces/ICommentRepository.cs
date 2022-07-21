using System.Linq.Expressions;
using FinalProject.Domain.Dtos;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Interfaces;

public interface ICommentRepository
{
    Task<int> Add(CommentDto model);
    Task<int> Update(CommentDto model);
    Task<int> Remove(int id);
    Task<CommentDto> GetById(int id);
    Task<IEnumerable<CommentDto>> GetAll();
}