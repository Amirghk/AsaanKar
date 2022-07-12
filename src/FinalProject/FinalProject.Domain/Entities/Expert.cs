using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

namespace FinalProject.Domain.Entities;

public class Expert
{
    #region Properties
    public int Id { get; set; }
    public float? Rating { get; set; }
    public int Votes { get; set; }
    public string? Bio { get; set; }
    public bool IsActive { get; set; }


    #endregion
    #region Navigational Properties
    public virtual Guid ExpertId { get; set; }
    public virtual Address Address { get; set; } = null!;
    public int? ProfilePictureId { get; set; }
    public virtual ICollection<Service>? Services { get; set; }
    public virtual ICollection<ServiceExpert>? ServiceExperts { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<FileDetails>? Pictures { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
    #endregion
}