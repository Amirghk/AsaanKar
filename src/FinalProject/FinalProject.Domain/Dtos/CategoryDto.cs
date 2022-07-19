
namespace FinalProject.Domain.Dtos;

public record CategoryDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public bool IsAvailable { get; init; }
    public int? ParentCategoryId { get; init; }
    public int? PictureId { get; init; }
}