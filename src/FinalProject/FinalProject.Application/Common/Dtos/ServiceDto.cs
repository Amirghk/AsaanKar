namespace FinalProject.Application.Common.Dtos;

public class ServiceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public int? ParentServiceId { get; set; }
    public int? FileInfoId { get; set; }
}