namespace FinalProject.Application.Common.Dtos;

public record CommentDto
{
    public int Id { get; init; }
    public string Content { get; init; } = null!;
    public int Votes { get; init; }
    public int CustomerId { get; init; }
    public int ExpertId { get; init; }
    public int? ImageId { get; init; }
}