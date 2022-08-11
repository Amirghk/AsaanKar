using FinalProject.Endpoint.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Models
{
    public class CategorySaveViewModel
    {
        [Display(Name = "نام")]
        public string Name { get; init; } = null!;
        [Display(Name = "توضیحات")]
        public string Description { get; set; } = null!;
        [Display(Name = "دسته بندی والد")]
        public int? ParentCategoryId { get; init; }
        [Display(Name = "تصویر")]
        [DataType(DataType.Upload)]
        [UploadFileExtentions(".png,.jpg,.jpeg,.gif")]
        public IFormFile? Picture { get; set; }
    }
}
