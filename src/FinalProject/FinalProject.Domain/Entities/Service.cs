using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

public class Service : IBaseEntity, ISoftDeletable
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region NavigationalProperties
    public int? ParentServiceId { get; set; }
    public virtual Service? ParentService { get; set; }
    public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();
    public virtual FileDetail? FileDetails { get; set; }
    public virtual ICollection<SubService> SubServices { get; set; } = new HashSet<SubService>();
    public virtual ICollection<Expert> Experts { get; set; } = new HashSet<Expert>();
    public virtual ICollection<ServiceExpert> ServiceExperts { get; set; } = new HashSet<ServiceExpert>();
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

    #endregion
}