using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Dtos;

public class AddressDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int Zip { get; set; }
    public AddressCategory AddressCategory { get; set; }
    public int? CustomerId { get; set; }
    public int? ExpertId { get; set; }
    public int CityId { get; set; }
    public int ProvinceId { get; set; }
}