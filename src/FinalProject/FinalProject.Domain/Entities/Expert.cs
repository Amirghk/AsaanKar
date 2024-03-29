using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;
/// <summary>
/// expert entity that references a Asp.net core user entity with ExpertId guid
/// , stores additional info for experts
/// </summary>
public class Expert : IBaseEntity, ISoftDeletable
{
    #region Properties
    public virtual string? Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string NationalCode { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public float? Rating { get; set; }
    public int Votes { get; set; }
    public string? Bio { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime BirthDate { get; set; }

    #endregion
    #region Navigational Properties
    public virtual Address? Address { get; set; }
    public int? ProfilePictureId { get; set; }
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    public virtual ICollection<ServiceExpert> ServiceExperts { get; set; } = new List<ServiceExpert>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<Upload> WorkSamples { get; set; } = new List<Upload>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    #endregion
}