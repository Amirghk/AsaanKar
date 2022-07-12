using FinalProject.Domain.Enums;

namespace FinalProject.Domain.Entities;

public class Service
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsAvailable { get; set; }
    #endregion

    #region NavigationalProperties
    public int? ParentServiceId { get; set; }
    public virtual Service? ParentService { get; set; }
    public int? FileInfoId { get; set; }
    public virtual FileDetails? FileInfo { get; set; }
    public virtual ICollection<SubService> SubServices { get; set; } = null!;
    public virtual ICollection<Expert>? Experts { get; set; }
    public virtual ICollection<ServiceExpert> ServiceExperts { get; set; } = null!;

    #endregion
}