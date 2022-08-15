using FinalProject.Endpoint.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Identity.Models
{
    public class ProfilePictureSaveViewModel
    {
        [Display(Name = "انتخاب عکس پروفایل")]
        [DataType(DataType.Upload)]
        [UploadFileExtentions(".png,.jpg,.jpeg,.gif")]
        public IFormFile ProfilePic { get; set; } = null!;
    }
}
