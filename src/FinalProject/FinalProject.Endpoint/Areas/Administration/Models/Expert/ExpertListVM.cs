using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Administration.Models
{
    public record ExpertListVM
    {
        [Display(Name = "شناسه")]
        public string Id { get; init; } = string.Empty;
        [Display(Name = "نام")]
        public string FirstName { get; init; } = string.Empty;
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "تاریخ تولد")]
        public string? BirthDate { get; set; }
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; } = string.Empty;
        [Display(Name = "شهر")]
        public string City { get; set; } = string.Empty;
    }
}
