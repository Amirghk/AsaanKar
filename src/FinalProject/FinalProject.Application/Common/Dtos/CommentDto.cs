namespace FinalProject.Application.Common.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int Votes { get; set; }
    public int CustomerId { get; set; }
    public int ExpertId { get; set; }
    public int? FileInfoId { get; set; }
}