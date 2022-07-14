using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Dtos;

public record FileDetailDto
{
    public int Id { get; init; }
    public string FileName { get; init; } = null!;
    public long FileSize { get; init; }
    public FileCategory FileCategory { get; init; }
    public int? CommentId { get; init; }
    public int? CustomerId { get; init; }
    public int? ExpertId { get; init; }
}