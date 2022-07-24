using FinalProject.Domain.Interfaces;
namespace FinalProject.Domain.Entities;
/// <summary>
/// entity for comments that has an expert, customer and also has an optional image field
/// </summary>
public class Comment : IAuditableEntity, IBaseEntity, ISoftDeletable
{
    #region Properties
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int Votes { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsApproved { get; set; }
    #endregion
    #region Navigational Properties
    public string CustomerId { get; set; } = null!;
    public virtual Customer Customer { get; set; } = null!;
    public string ExpertId { get; set; } = null!;
    public virtual Expert Expert { get; set; } = null!;
    public int? ImageId { get; set; }
    public Upload? Image { get; set; }
    #endregion
}