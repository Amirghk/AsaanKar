using FinalProject.Application.Common.Dtos;

namespace FinalProject.Application.Common.Interfaces.Services;

public interface ICommentService
{
    Task<int> Set(CommentDto dto);
    Task<IEnumerable<CommentDto>> GetAll();
    Task<CommentDto> GetById(int id);
    Task<int> Remove(int id);
    Task<int> Update(CommentDto dto);
}
