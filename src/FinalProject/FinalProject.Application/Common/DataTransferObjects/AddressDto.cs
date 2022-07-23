using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.DataTransferObjects;

public record AddressDto
{
    public int Id { get; init; }
    public string Content { get; init; } = null!;
    public string PostalCode { get; init; } = null!;
    public AddressCategory AddressCategory { get; init; }
    public string? CustomerId { get; init; }
    public string? ExpertId { get; init; }
    public int CityId { get; init; }
}