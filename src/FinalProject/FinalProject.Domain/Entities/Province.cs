namespace FinalProject.Domain.Entities;

public class Province
{
    #region Property
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsSupported { get; set; }
    #endregion
    #region Navigational Property
    public virtual ICollection<City>? Cities { get; set; }
    #endregion
}