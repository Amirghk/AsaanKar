using FinalProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Identity.Models
{
    public record UploadViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; init; }
        [Display(Name = "نام فایل")]
        public string FileName { get; init; } = null!;
        [Display(Name = "سایز فایل")]
        public long FileSize { get; init; }
        [Display(Name = "نوع فایل")]
        public FileCategory FileCategory { get; init; }
    }
}
