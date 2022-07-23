using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.Enums;

public enum OrderState
{
    [Display(Name = "منتظر پیشنهاد متخصص")]
    WaitingForExpertBid = 1,
    [Display(Name = "منتظر انتتخاب متخصص")]
    WaitingToChooseExpert = 2,
    [Display(Name = "منتظر رسیدن متخصص")]
    WaitingForExpertToArrive = 3,
    [Display(Name = "کار شروع شده")]
    WorkStarted = 4,
    [Display(Name = "کار تمام شده")]
    WorkFinished = 5,
    [Display(Name = "پرداخت شده")]
    Paid = 6,
    [Display(Name = "کنسل شده توسط متخصص")]
    CanceledByExpert = 7,
    [Display(Name = "کنسل شده توسط مشتری")]
    CanceledByCustomer = 8,
    [Display(Name = "کنسل شده توسط ادمین")]
    CanceledByAdmin = 9
}