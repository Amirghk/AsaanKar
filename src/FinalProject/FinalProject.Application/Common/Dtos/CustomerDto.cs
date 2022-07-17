namespace FinalProject.Application.Common.Dtos;

public record CustomerDto
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public string? CustomerId { get; init; }
    public int? FileInfoId { get; init; }
    public DateTime? Birthdate { get; set; }
}
