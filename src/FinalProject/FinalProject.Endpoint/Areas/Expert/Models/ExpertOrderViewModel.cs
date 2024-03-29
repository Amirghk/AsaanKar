﻿using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Expert.Models
{
    public class ExpertOrderViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "نام سرویس")]
        public string ServiceName { get; set; } = string.Empty;
        [Display(Name = "مشتری")]
        public CustomerDto? Customer { get; set; }
        [Display(Name = "تاریخ سفارش")]
        public DateTimeOffset OrderDate { get; set; }
        [Display(Name = "تاریخ نیاز به سرویس")]
        public DateTimeOffset DateRequired { get; set; }
        [Display(Name = "تاریخ اتمام سفارش")]
        public DateTimeOffset? DateCompleted { get; set; }
        [Display(Name = "وضعیت سفارش")]
        public OrderState State { get; set; }
        [Display(Name = "قیمت پایه")]
        public decimal ServiceBasePrice { get; set; }
        [Display(Name = "قیمت انجام شده")]
        public decimal? CompletedPrice { get; set; }
        [Display(Name = "آدرس")]
        public int AddressId { get; set; }
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        public bool HasBidAlready { get; set; }
        public bool IsDeletable { get; set; }
    }
}
