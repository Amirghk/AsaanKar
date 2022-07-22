using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;
/// <summary>
/// each order has a completion date plus audit properties, also has a state showing what 
/// state the order is in ( canceled, finished, ...)
/// each order has an expert, customer, address and a service
/// </summary>
public class Order : IAuditableEntity, IBaseEntity, ISoftDeletable
{
    #region Properties
    public int Id { get; set; }
    public DateTimeOffset? DateCompleted { get; set; }
    public string? Description { get; set; }
    public OrderState State { get; set; }
    public bool IsDeleted { get; set; }
    public decimal ServiceBasePrice { get; set; }
    public decimal? CompletedPrice { get; set; }
    #endregion

    #region Navigational Properties
    public int AddressId { get; set; }
    public virtual Address ReceiverAddress { get; set; } = null!;
    // made ServiceId nullable to allow for null value in case deleted service is used in order
    public int? ServiceId { get; set; }
    public virtual Service Service { get; set; } = null!;
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    public int ExpertId { get; set; }
    public virtual Expert Expert { get; set; } = null!;
    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
    #endregion
}
