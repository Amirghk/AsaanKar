using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

public class Address : ISoftDeletable, IBaseEntity
{
    #region Properties
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public AddressCategory AddressCategory { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigational Properties
    public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public int? ExpertId { get; set; }
    public virtual Expert? Expert { get; set; }
    public int CityId { get; set; }
    // TODO : Make relations nullable
    public virtual City City { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    #endregion
}