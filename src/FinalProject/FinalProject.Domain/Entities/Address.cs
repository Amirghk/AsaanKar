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
    public Customer? Customer { get; set; }
    public int? ExpertId { get; set; }
    public Expert? Expert { get; set; }
    public int CityId { get; set; }
    public City City { get; set; } = null!;
    #endregion
}