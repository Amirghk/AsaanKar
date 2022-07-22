
namespace FinalProject.Application.Common.DataTransferObjects;

public record ProvinceDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public bool IsSupported { get; init; }
}