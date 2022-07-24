using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Administration.Models
{
    public class BidViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; init; }
        [Display(Name = "توضیحات")]
        public string? Notes { get; init; }
        [Display(Name = "قیمت")]
        public decimal Price { get; init; }
        [Display(Name = "شناسه سفارش")]
        public int OrderId { get; init; }
        [Display(Name = "شناسه متخصص")]
        public string? ExpertId { get; init; }
    }
}
