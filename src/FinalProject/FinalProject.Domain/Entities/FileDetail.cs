using System.Runtime.CompilerServices;
using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

// TODO : Uploads
public class FileDetail : IAuditableEntity, IBaseEntity
{
    #region Properties
    public int Id { get; set; }
    public string FileName { get; set; } = null!;
    public long FileSize { get; set; }
    public FileCategory FileCategory { get; set; }
    #endregion
    #region Navigational Properties
    //public int? CommentId { get; set; }
    public virtual Comment? Comment { get; set; }
    public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public int? ExpertId { get; set; }
    public virtual Expert? Expert { get; set; }
    public int? ServiceId { get; set; }
    public virtual Service? Service { get; set; }
    #endregion

}