using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Models
{
    public class AddressListViewModel
    {
        public int Id { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; } = string.Empty;
    }
}
