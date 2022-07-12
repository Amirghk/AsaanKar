using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;

namespace FinalProject.Domain.Entities;

public class Customer
{
    #region Properties
    public int Id { get; set; }
    #endregion
    #region Navigational Properties
    public Guid CustomerId { get; set; }
    public virtual ICollection<Address> Addresses { get; set; } = null!;
    public int? FileInfoId { get; set; }
    public virtual FileDetails? ProfilePicture { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
    #endregion
}