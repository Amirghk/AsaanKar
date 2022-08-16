namespace FinalProject.Endpoint.Models
{
    public class ServiceSaveViewModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; init; } = null!;
        public decimal BasePrice { get; set; }
        public int CategoryId { get; init; }
    }
}
