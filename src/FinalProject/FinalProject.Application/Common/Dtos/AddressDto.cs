namespace FinalProject.Application.Common.Dtos;

public class AddressDto
{
    public int Id { get; set; }
    public string Direction { get; set; } = null!;
    public string Zip { get; set; } = null!;
#endregion

    #region Navigational Properties
    public int UserId { get; set; }
    // public User User { get; set; } = null!;
    public int CityId { get; set; }
    public City City { get; set; } = null!;
    public int ProvinceId { get; set; }
    public Province Province { get; set; } = null!;
}