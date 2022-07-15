using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Dtos;

public record AddressDto
{
    public int Id { get; init; }
    public string Content { get; init; } = null!;
    public string PostalCode { get; init; } = null!;
    public AddressCategory AddressCategory { get; init; }
    public int? CustomerId { get; init; }
    public int? ExpertId { get; init; }
    public int CityId { get; init; }
}