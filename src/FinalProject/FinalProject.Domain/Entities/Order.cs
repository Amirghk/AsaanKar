using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

public class Order : IAuditableEntity, IBaseEntity
{
    #region Properties
    public int Id { get; set; }
    public DateTimeOffset DateAdded { get; set; }
    public DateTimeOffset? DateCompleted { get; set; }
    public string? Description { get; set; }
    public OrderState State { get; set; }
    #endregion

    #region Navigational Properties
    public int AddressId { get; set; }
    public Address ReceiverAddress { get; set; } = null!;
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public int ExpertId { get; set; }
    public Expert Expert { get; set; } = null!;
    #endregion
}
