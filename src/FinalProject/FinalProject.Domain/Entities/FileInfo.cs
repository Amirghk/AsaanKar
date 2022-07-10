namespace FinalProject.Domain.Entities;

public class FileInfo
{
    public int Id { get; set; }
    public string FileName { get; set; } = null!;
    public long FileSize { get; set; }
}