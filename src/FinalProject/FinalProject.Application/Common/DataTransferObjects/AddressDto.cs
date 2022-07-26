using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.DataTransferObjects;

public record AddressDto
{
    public int Id { get; init; }
    public string Content { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public AddressCategory AddressCategory { get; set; }
    public string? CustomerId { get; set; }
    public string? ExpertId { get; set; }
    public int CityId { get; set; }
}