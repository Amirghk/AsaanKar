namespace FinalProject.Application.Common.Dtos;

public record CustomerDto
{
    public int Id { get; init; }
    public Guid CustomerId { get; init; }
    public int? FileInfoId { get; init; }
}
