namespace FinalProject.Domain.Entities;

public class Order
{
    #region Properties
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
    #endregion

    #region Navigational Properties
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    // TODO
    #endregion



}
