namespace FinalProject.Application.Common.Dtos;

public record ServiceDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public bool IsAvailable { get; init; }
    public int? ParentServiceId { get; init; }
    public int? FileInfoId { get; init; }
}