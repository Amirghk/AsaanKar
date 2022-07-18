using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;
/// <summary>
/// customer entity that references a Asp.net core user entity with CustomerId guid
/// , stores additional info for customers
/// </summary>
public class Customer : IBaseEntity
{
    #region Properties
    public int Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int? FileInfoId { get; init; }
    public DateTime? BirthDate { get; set; }
    #endregion
    #region Navigational Properties
    public string? CustomerId { get; set; }
    public int? ProfilePictureId { get; set; }
    public virtual Upload? ProfilePicture { get; set; }
    public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    #endregion
}