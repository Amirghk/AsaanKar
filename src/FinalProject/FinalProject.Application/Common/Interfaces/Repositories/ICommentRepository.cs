using System.Linq.Expressions;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Application.Common.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<int> Add(CommentDto model);
    Task<int> Update(CommentDto model);
    Task<int> Remove(int id);
    Task<CommentDto> GetById(int id, CancellationToken cancellationToken);
    Task<IEnumerable<CommentDto>> GetAll(CancellationToken cancellationToken);
}