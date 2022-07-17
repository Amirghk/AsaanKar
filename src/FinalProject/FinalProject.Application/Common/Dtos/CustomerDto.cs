namespace FinalProject.Application.Common.Dtos;

public record CustomerDto
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public Guid CustomerId { get; init; }
    public int? FileInfoId { get; init; }
    public DateTime? Birthdate { get; set; }
}
