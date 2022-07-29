using FinalProject.Application.Common.DataTransferObjects;
namespace FinalProject.Application.Common.Interfaces.Services;

public interface ICommentService
{
    Task<int> Set(CommentDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<CommentDto>> GetAll(CancellationToken cancellationToken);
    Task<CommentDto> GetById(int id, CancellationToken cancellationToken);
    Task<int> Remove(int id, CancellationToken cancellationToken);
    Task<int> Update(CommentDto dto, CancellationToken cancellationToken);
    Task<int> Approve(int id, CancellationToken cancellationToken);
}
