
namespace FinalProject.Domain.Dtos;

public record CityDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public bool IsSupported { get; init; }
    public int ProvinceId { get; init; }
}