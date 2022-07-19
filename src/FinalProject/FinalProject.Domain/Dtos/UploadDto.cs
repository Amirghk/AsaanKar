using FinalProject.Domain.Enums;


namespace FinalProject.Domain.Dtos;

public record UploadDto
{
    public int Id { get; init; }
    public string FileName { get; init; } = null!;
    public long FileSize { get; init; }
    public FileCategory FileCategory { get; init; }
    public int? ExpertId { get; init; }
}