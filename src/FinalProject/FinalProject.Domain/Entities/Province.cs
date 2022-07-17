using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;
/// <summary>
/// each province has a name and a collection of cities
/// and an IsSupported field showing if the app offers services in that province
/// </summary>
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