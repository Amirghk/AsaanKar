using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Models
{
    public class CategoryViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; init; }
        [Display(Name = "نام")]
        public string Name { get; init; } = null!;
        [Display(Name = "توضیحات")]
        public string Description { get; set; } = null!;
        [Display(Name = "دسته بندی والد")]
        public int? ParentCategoryId { get; init; }
        [Display(Name = "تصویر")]
        public int? PictureId { get; set; }
        public List<ServiceListViewModel> Services { get; set; } = new List<ServiceListViewModel>();
    }
}
