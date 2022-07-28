using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Administration.Models
{
    public class UserEditViewModel
    {
        [Phone]
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "نام")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; } = null!;
        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "کدملی")]
        public string NationalCode { get; set; } = String.Empty;
        public bool IsCustomer { get; set; } = false;
        public bool IsExpert { get; set; } = false;
        [Display(Name = "نام کاربری")]
        public string Username { get; set; } = string.Empty;
        [Display(Name = "ایمیل")]
        public string Email { get; set; } = string.Empty;
    }
}
