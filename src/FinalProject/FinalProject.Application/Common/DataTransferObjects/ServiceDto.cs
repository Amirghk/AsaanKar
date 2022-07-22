using System.ComponentModel;

namespace FinalProject.Application.Common.DataTransferObjects;

public record ServiceDto
{
    public int Id { get; init; }
    public string Description { get; init; } = null!;
    public decimal? BasePrice { get; set; }
    public int CategoryId { get; init; }
}