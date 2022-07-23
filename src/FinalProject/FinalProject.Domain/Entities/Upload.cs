using System.Runtime.CompilerServices;
using FinalProject.Domain.Enums;
using FinalProject.Domain.Interfaces;

namespace FinalProject.Domain.Entities;

/// <summary>
/// stores file name and size. also has a category property that stores what category of file
/// this is(customer, category, expert...)
/// </summary>
public class Upload : IAuditableEntity, IBaseEntity
{
    #region Properties
    public int Id { get; set; }
    public string FileName { get; set; } = null!;
    public long FileSize { get; set; }
    public FileCategory FileCategory { get; set; }
    #endregion
    #region Navigational Properties
    public virtual Comment? Comment { get; set; }
    public virtual Customer? Customer { get; set; }
    public string? ExpertId { get; set; }
    public virtual Expert? Expert { get; set; }
    public virtual Category? Category { get; set; }
    #endregion

}