
using FinalProject.Endpoint.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Models
{
    public class CommentSaveViewModel
    {
        [Display(Name = "محتوا")]
        public string Content { get; set; } = null!;
        public string ExpertId { get; init; } = null!;
        [Display(Name = "بارگذاری تصویر")]
        [DataType(DataType.Upload)]
        [UploadFileExtentions(".png,.jpg,.jpeg,.gif")]
        public IFormFile? Image { get; set; }
    }
}
