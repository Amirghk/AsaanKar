
namespace FinalProject.Domain.Dtos;

public record ExpertDto
{
    public int Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string NationalCode { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public float? Rating { get; init; }
    public int Votes { get; init; }
    public string? Bio { get; init; }
    public bool IsActive { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? ExpertId { get; init; }
    public int? ProfilePictureId { get; init; }
    public bool IsDeleted { get; init; }
}
