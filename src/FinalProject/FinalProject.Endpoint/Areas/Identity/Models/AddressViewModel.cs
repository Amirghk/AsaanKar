using FinalProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Identity.Models
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "آدرس")]
        public string Content { get; set; } = null!;
        [DataType(DataType.PostalCode)]
        [Display(Name = "کدپستی")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string PostalCode { get; set; } = null!;
        public string? CustomerId { get; set; }
        public string? ExpertId { get; set; }
        [Display(Name = "شهر")]
        public int CityId { get; set; }
        [Display(Name = "استان")]
        public int ProvinceId { get; set; }
    }
}
