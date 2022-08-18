using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Administration.Models
{
    public record CustomerListVM
    {
        [Display(Name = "شناسه")]
        public string Id { get; init; } = string.Empty;
        [Display(Name = "نام")]
        public string FirstName { get; init; } = null!;
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; } = null!;
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; } = null!;
        [Display(Name = "تاریخ تولد")]
        public string? BirthDate { get; set; }
    }
}
