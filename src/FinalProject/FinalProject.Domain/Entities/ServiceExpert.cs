using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

public class ServiceExpert : IAuditableEntity
{
    #region Properties
    #endregion
    #region NavigationalProperties
    public int ServiceId { get; set; }
    public virtual Service Service { get; set; } = null!;
    public int ExpertId { get; set; }
    public virtual Expert Expert { get; set; } = null!;
    #endregion
}