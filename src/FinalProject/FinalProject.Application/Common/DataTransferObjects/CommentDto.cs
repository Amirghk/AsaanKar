
namespace FinalProject.Application.Common.DataTransferObjects;

public record CommentDto
{
    public int Id { get; init; }
    public string Content { get; set; } = null!;
    public int Votes { get; set; }
    public int CustomerId { get; init; }
    public int ExpertId { get; init; }
    public int? ImageId { get; set; }
}