using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;
/// <summary>
/// customer entity that references a Asp.net core user entity with CustomerId guid
/// , stores additional info for customers
/// </summary>
public class Customer : IBaseEntity, ISoftDeletable
{
    #region Properties
    public string? Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int? FileInfoId { get; set; }
    public DateTime? BirthDate { get; set; }
    public bool IsDeleted { get; set; }
    #endregion
    #region Navigational Properties
    public int? ProfilePictureId { get; set; }
    public virtual Upload? ProfilePicture { get; set; }
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    #endregion
}