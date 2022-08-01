
namespace FinalProject.Application.Common.DataTransferObjects;

public record ExpertDto
{
    public string Id { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string NationalCode { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public float? Rating { get; set; }
    public int Votes { get; set; }
    public string? Bio { get; set; }
    public bool IsActive { get; set; }
    public DateTime? BirthDate { get; init; }
    public int? ProfilePictureId { get; set; }
    public bool IsDeleted { get; set; }
    public AddressDto? Address { get; set; }
    public List<ServiceDto> Services { get; set; } = new List<ServiceDto>();
    public virtual ICollection<UploadDto> WorkSamples { get; set; } = new List<UploadDto>();
    public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
}
