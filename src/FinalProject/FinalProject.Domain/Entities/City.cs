
namespace FinalProject.Domain.Entities;

public class City
{
    #region Property
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsSupported { get; set; }
    #endregion
    #region Navigational Property
    public int ProvinceId { get; set; }
    public Province Province { get; set; } = null!;
    #endregion
}