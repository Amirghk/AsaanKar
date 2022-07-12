using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Dtos;

public class FileInfoDto
{
    public int Id { get; set; }
    public string FileName { get; set; } = null!;
    public long FileSize { get; set; }
    public FileCategory FileCategory { get; set; }
    public int? CommentId { get; set; }
    public int? CustomerId { get; set; }
    public int? ExpertId { get; set; }
}