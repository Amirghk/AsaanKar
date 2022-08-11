
namespace FinalProject.Application.Common.DataTransferObjects;

public record CityDto
{
    public int Id { get; set; }
    public string Name { get; init; } = null!;
    public bool IsSupported { get; init; }
    public int ProvinceId { get; init; }
}