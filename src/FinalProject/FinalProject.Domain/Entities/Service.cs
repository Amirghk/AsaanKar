using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;
/// <summary>
/// each service has a parent category and also has a suggested price
/// </summary>
public class Service : IBaseEntity, ISoftDeletable
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal BasePrice { get; set; }
    public bool IsDeleted { get; set; }
    #endregion
    #region Navigational Properties
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<Expert> Experts { get; set; } = new List<Expert>();
    public virtual ICollection<ServiceExpert> ServiceExperts { get; set; } = new List<ServiceExpert>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    #endregion
}