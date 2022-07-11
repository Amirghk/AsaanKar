using System.Runtime.CompilerServices;
using FinalProject.Domain.Enums;

namespace FinalProject.Domain.Entities;

public class FileInfo
{
    #region Properties
    public int Id { get; set; }
    public string FileName { get; set; } = null!;
    public long FileSize { get; set; }
    public FileCategory FileCategory { get; set; }
    #endregion
    #region Navigational Properties
    public int? CommentId { get; set; }
    public Comment? Comment { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int? ExpertId { get; set; }
    public Expert? Expert { get; set; }
    #endregion

}