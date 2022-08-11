namespace FinalProject.Endpoint.Models
{
    public class ServiceSaveViewModel
    {
        public string Description { get; init; } = null!;
        public decimal BasePrice { get; set; }
        public int CategoryId { get; init; }
    }
}
