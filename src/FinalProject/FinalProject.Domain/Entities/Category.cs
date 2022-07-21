using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;
/// <summary>
///  services table has a tree relationship with itself 
///  allowing each service to have parent services and child services
///  it also has a collection of experts and orders and subservices
/// </summary>
public class Category : IBaseEntity, ISoftDeletable
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region NavigationalProperties
    public int? ParentCategoryId { get; set; }
    public virtual Category? ParentCategory { get; set; }
    public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    public int? PictureId { get; set; }
    public virtual Upload? FileDetails { get; set; }
    public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();

    #endregion
}