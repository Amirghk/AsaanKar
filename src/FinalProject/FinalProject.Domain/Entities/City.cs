
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

public class City : IBaseEntity, ISoftDeletable
{
    #region Property
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsSupported { get; set; }
    public bool IsDeleted { get; set; }
    #endregion
    #region Navigational Property
    public int ProvinceId { get; set; }
    public virtual Province Province { get; set; } = null!;
    public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    #endregion
}