using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Areas.Administration.Pages.Uploads
{
    public class IndexModel : PageModel
    {
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public IndexModel(IUploadService uploadService, IMapper mapper, IWebHostEnvironment environment)
        {
            _uploadService = uploadService;
            _mapper = mapper;
            _environment = environment;
        }


        public List<UploadViewModel> UploadViewModels { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            UploadViewModels = _mapper.Map<List<UploadViewModel>>(await _uploadService.GetAll());
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "Uploads");
            await _uploadService.Remove(id, uploadsRootFolder);
            return RedirectToPage();
        }
    }
}
