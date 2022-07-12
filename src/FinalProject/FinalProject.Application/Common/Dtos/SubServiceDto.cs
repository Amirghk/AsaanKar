namespace FinalProject.Application.Common.Dtos;

public class SubServiceDto
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public long? Price { get; set; }
    public int ServiceId { get; set; }

}