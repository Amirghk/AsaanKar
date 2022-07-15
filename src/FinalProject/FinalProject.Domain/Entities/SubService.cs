using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

public class SubService : IBaseEntity, ISoftDeletable
{
    #region Properties
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public long? Price { get; set; }
    public bool IsDeleted { get; set; }
    #endregion
    #region Navigational Properties
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    #endregion
}