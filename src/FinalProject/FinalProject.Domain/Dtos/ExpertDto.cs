
namespace FinalProject.Domain.Dtos;

public record ExpertDto
{
    public int Id { get; init; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string NationalCode { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public float? Rating { get; init; }
    public int Votes { get; init; }
    public string? Bio { get; init; }
    public bool IsActive { get; init; }
    public DateTime? BirthDate { get; set; }
    public string? ExpertId { get; init; }
    public int? ProfilePictureId { get; init; }
}
