using FinalProject.Endpoint.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Expert.Models
{
    public class ExpertWorkSampleViewModel
    {

        [Display(Name = "آپلود نمونه کار")]
        [DataType(DataType.Upload)]
        [UploadFileExtentions(".png,.jpg,.jpeg,.gif")]
        public IFormFile WorkSample { get; set; } = null!;

    }
}
