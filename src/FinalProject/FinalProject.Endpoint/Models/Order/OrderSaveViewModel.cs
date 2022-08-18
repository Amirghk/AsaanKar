using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Models
{
    public class OrderSaveViewModel
    {
        [Display(Name = "چه زمانی به سرویس نیاز دارید؟")]
        public string DateRequired { get; set; } = null!;
        [Display(Name = "توضیحات")]
        public string? Description { get; init; }
        public int AddressId { get; set; }
        public int ServiceId { get; set; }
        public string? CustomerId { get; init; } = null!;
    }
}
