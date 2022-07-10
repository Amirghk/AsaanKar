namespace FinalProject.Domain.Entities;

public class Province
{
    #region Property
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsSupported { get; set; }
    #endregion
    #region Navigational Property
    public City Cities { get; set; } = null!;
    #endregion
}