using FinalProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Administration.Models
{
    public class OrderEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        //[Display(Name = "شناسه مشتری")]
        //public string CustomerId { get; set; } = String.Empty;
        //[Display(Name = "شناسه متخصص")]
        //public string? ExpertId { get; set; }
        [Display(Name = "تاریخ تکمیل سفارش")]
        public DateTimeOffset? DateCompleted { get; set; }
        [Display(Name = "وضعیت سفارش")]
        public OrderState State { get; set; }
        //[Display(Name = "قیمت پایه")]
        //public decimal ServiceBasePrice { get; set; }
        //[Display(Name = "قیمت انجام شده")]
        //public decimal? CompletedPrice { get; set; }
        //[Display(Name = "توضیحات")]
        //public string? Description { get; set; }
    }
}
