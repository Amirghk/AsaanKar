using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Administration.Models
{
    public class OrderListViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "نام سرویس")]
        public string ServiceName { get; set; } = string.Empty;
        [Display(Name = "شناسه مشتری")]
        public string CustomerId { get; set; } = null!;
        [Display(Name = "شناسه متخصص")]
        public string? ExpertId { get; set; }
        [Display(Name = "تاریخ سفارش")]
        public DateTimeOffset OrderDate { get; set; }
        [Display(Name = "تاریخ اتمام سفارش")]
        public DateTimeOffset? DateCompleted { get; set; }
        [Display(Name = "وضعیت سفارش")]
        public OrderState State { get; set; }
        [Display(Name = "قیمت پایه")]
        public decimal ServiceBasePrice { get; set; }
        [Display(Name = "قیمت انجام شده")]
        public decimal? CompletedPrice { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; } = string.Empty;
        [Display(Name = "پیشنهادات قیمت")]
        public ICollection<BidDto> Bids { get; set; } = new List<BidDto>();
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }
    }
}
