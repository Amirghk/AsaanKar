
namespace FinalProject.Application.Common.DataTransferObjects;

public record CommentDto
{
    public int Id { get; init; }
    public string Content { get; set; } = null!;
    public int Votes { get; set; }
    public string CustomerId { get; set; } = null!;
    public string ExpertId { get; set; } = null!;
    public int? ImageId { get; set; }
    public UploadDto? Image { get; set; }
    public bool IsApproved { get; set; }
}