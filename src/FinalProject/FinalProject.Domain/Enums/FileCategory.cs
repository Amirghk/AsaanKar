using System.ComponentModel.DataAnnotations;

namespace FinalProject.Domain.Enums;

public enum FileCategory
{
    [Display(Name = "عکس پروفایل کاربر")]
    Customer = 1,
    [Display(Name = "عکس نمونه کار متخصص")]
    ExpertWorkSample = 2,
    [Display(Name = "عکس الحاقی کامنت")]
    Comment = 3,
    [Display(Name = "عکس دسته بندی سرویس")]
    ServiceCategory = 4,
    [Display(Name = "عکس پروفایل متخصص")]
    ExpertProfilePic = 5,
}
