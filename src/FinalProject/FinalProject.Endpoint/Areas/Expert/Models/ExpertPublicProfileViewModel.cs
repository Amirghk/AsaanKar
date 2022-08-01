using FinalProject.Endpoint.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Expert.Models
{
    public class ExpertPublicProfileViewModel
    {
        public string Id { get; set; } = null!;
        [Display(Name = "نام")]
        public string? Name { get; set; }
        [Display(Name = "معرفی")]
        public string? Bio { get; set; }
        [Display(Name = "کامنت ها")]
        public List<CommentListViewModel>? Comments { get; set; }
        [Display(Name = "نمونه کارها")]
        public List<int>? WorkSamples { get; set; }
        [Display(Name = "امتیاز")]
        public float? Rating { get; set; }
        public List<string>? Services { get; set; }
        public int? ProfilePictureId { get; set; }
    }
}
