using FinalProject.Domain.Enums;

namespace FinalProject.Domain.Entities;

public class Address
{
    #region Properties
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int Zip { get; set; }
    public AddressCategory AddressCategory { get; set; }
    #endregion

    #region Navigational Properties
    public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public int? ExpertId { get; set; }
    public virtual Expert? Expert { get; set; }
    public int CityId { get; set; }
    public virtual City City { get; set; } = null!;
    public virtual ICollection<Order>? Orders { get; set; }
    #endregion
}