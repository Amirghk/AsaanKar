
namespace FinalProject.Domain.Dtos;

public record ExpertDto
{
    public int Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string NationalCode { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public float? Rating { get; set; }
    public int Votes { get; set; }
    public string? Bio { get; set; }
    public bool IsActive { get; set; }
    public DateTime? BirthDate { get; init; }
    public string? ExpertId { get; init; }
    public int? ProfilePictureId { get; set; }
    public bool IsDeleted { get; set; }
}
