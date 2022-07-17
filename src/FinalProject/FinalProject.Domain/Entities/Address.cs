using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.Entities;
/// <summary>
/// entity containing addresses and their respective city and provinces
/// also has a category showing what user it belongs to
/// </summary>
public class Address : ISoftDeletable, IBaseEntity
{
    #region Properties
    [Display(Name = "شناسه")]
    public int Id { get; set; }
    [Display(Name = "محتویات")]
    public string Content { get; set; } = null!;
    [Display(Name = "کد پستی")]
    public string PostalCode { get; set; } = null!;
    [Display(Name = "نوع آدرس")]
    public AddressCategory AddressCategory { get; set; }
    [Display(Name = "پاک شده")]
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigational Properties
    public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public int? ExpertId { get; set; }
    public virtual Expert? Expert { get; set; }
    public int CityId { get; set; }
    public virtual City City { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    #endregion
}