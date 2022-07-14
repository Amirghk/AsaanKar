namespace FinalProject.Application.Common.Dtos;

public record ExpertDto
{
    public int Id { get; init; }
    public float? Rating { get; init; }
    public int Votes { get; init; }
    public string? Bio { get; init; }
    public bool IsActive { get; init; }
    public virtual Guid ExpertId { get; init; }
    public int? ProfilePictureId { get; init; }
}
