using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

public class Customer : IBaseEntity
{
    #region Properties
    public int Id { get; set; }
    #endregion
    #region Navigational Properties
    public Guid CustomerId { get; set; }
    public int? FileInfoId { get; set; }
    public virtual FileDetail? ProfilePicture { get; set; }
    public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    #endregion
}