using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;
/// <summary>
/// expert entity that references a Asp.net core user entity with ExpertId guid
/// , stores additional info for experts
/// </summary>
public class Expert : IBaseEntity
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public float? Rating { get; set; }
    public int Votes { get; set; }
    public string? Bio { get; set; }
    public bool IsActive { get; set; }
    public DateOnly BirthDate { get; set; }

    #endregion
    #region Navigational Properties
    public virtual string? ExpertId { get; set; }
    public virtual Address Address { get; set; } = null!;
    public int? ProfilePictureId { get; set; }
    public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();
    public virtual ICollection<ServiceExpert> ServiceExperts { get; set; } = new HashSet<ServiceExpert>();
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public virtual ICollection<Upload> Pictures { get; set; } = new HashSet<Upload>();
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    #endregion
}