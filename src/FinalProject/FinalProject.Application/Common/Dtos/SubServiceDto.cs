using System.ComponentModel;
namespace FinalProject.Application.Common.Dtos;

public record SubServiceDto
{
    public int Id { get; init; }
    public string Description { get; init; } = null!;
    public long? Price { get; init; }
    public int ServiceId { get; init; }
}