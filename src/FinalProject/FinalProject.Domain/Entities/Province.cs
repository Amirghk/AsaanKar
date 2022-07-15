using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

public class Province : IBaseEntity, ISoftDeletable
{
    #region Property
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsSupported { get; set; }
    public bool IsDeleted { get; set; }
    #endregion
    #region Navigational Property
    public virtual ICollection<City> Cities { get; set; } = new HashSet<City>();
    #endregion
}