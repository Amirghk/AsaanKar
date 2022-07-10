using FinalProject.Domain.Enums;

namespace FinalProject.Domain.Entities;

public class Service
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public OrderState OrderState { get; set; }
    #endregion

    #region NavigationalProperties
    public int? ParentServiceId { get; set; }
    public Service? ParentService { get; set; }
    #endregion
}