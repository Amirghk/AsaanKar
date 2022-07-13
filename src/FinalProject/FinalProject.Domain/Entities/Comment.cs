using FinalProject.Domain.Interfaces;
namespace FinalProject.Domain.Entities;

public class Comment : IAuditableEntity
{
    #region Properties
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int Votes { get; set; }
    #endregion
    #region Navigational Properties
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    public int ExpertId { get; set; }
    public virtual Expert Expert { get; set; } = null!;
    public int? ImageId { get; set; }
    public FileDetail? Image { get; set; }
    #endregion
}