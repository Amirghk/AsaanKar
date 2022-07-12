namespace FinalProject.Application.Common.Dtos;

public class CityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsSupported { get; set; }
    public int ProvinceId { get; set; }
}