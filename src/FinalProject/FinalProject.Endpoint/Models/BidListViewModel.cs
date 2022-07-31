using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Models
{
    public class BidViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; init; }
        [Display(Name = "توضیحات")]
        [MaxLength(500, ErrorMessage = "توضیحات باید حداکثر {0} کاراکتر باشد.")]
        public string? Notes { get; init; }
        [Required]
        [Range(1000, 1000000000000, ErrorMessage = "قیمت پیشنهادی باید بین {1} و {2} باشد.")]
        [Display(Name = "قیمت")]
        public decimal Price { get; init; }
        [Display(Name = "شناسه سفارش")]
        public int OrderId { get; set; }
        [Display(Name = "شناسه متخصص")]
        public string? ExpertId { get; set; }
    }
}
