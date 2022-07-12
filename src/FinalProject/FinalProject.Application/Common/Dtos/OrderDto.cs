using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public DateTimeOffset DateAdded { get; set; }
    public DateTimeOffset? DateCompleted { get; set; }
    public string? Description { get; set; }
    public OrderState State { get; set; }
    public int AddressId { get; set; }
    public int ServiceId { get; set; }
    public int CustomerId { get; set; }
    public int ExpertId { get; set; }
}