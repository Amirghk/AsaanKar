
namespace FinalProject.Application.Common.DataTransferObjects;

public record CategoryDto
{
    public int Id { get; set; }
    public string Name { get; init; } = null!;
    public string Description { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public int? ParentCategoryId { get; init; }
    public int? PictureId { get; set; }
    public virtual ICollection<ServiceDto> Services { get; set; } = new List<ServiceDto>();
}