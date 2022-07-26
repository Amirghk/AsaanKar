using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;
/// <summary>
/// an entity joining service and expert
/// by doing so each expert can support multiple services and each service can have mulltiple experts
/// </summary>
public class ServiceExpert : IAuditableEntity
{
    #region Properties
    #endregion
    #region NavigationalProperties
    public int ServiceId { get; set; }
    public virtual Service Service { get; set; } = null!;
    public string ExpertId { get; set; } = null!;
    public virtual Expert Expert { get; set; } = null!;
    #endregion
}