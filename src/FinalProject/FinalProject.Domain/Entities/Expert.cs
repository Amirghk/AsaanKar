using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

public class Expert : IBaseEntity
{
    #region Properties
    public int Id { get; set; }
    public float? Rating { get; set; }
    public int Votes { get; set; }
    public string? Bio { get; set; }
    public bool IsActive { get; set; }
    public DateTime BirthDate { get; set; }

    #endregion
    #region Navigational Properties
    public virtual Guid ExpertId { get; set; }
    public virtual Address Address { get; set; } = null!;
    public int? ProfilePictureId { get; set; }
    public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();
    public virtual ICollection<ServiceExpert> ServiceExperts { get; set; } = new HashSet<ServiceExpert>();
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public virtual ICollection<FileDetail> Pictures { get; set; } = new HashSet<FileDetail>();
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    #endregion
}