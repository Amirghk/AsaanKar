using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize("IsAdmin")]
    public class UploadController : Controller
    {
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public UploadController(IUploadService uploadService, IMapper mapper, IWebHostEnvironment environment)
        {
            _uploadService = uploadService;
            _mapper = mapper;
            _environment = environment;
        }



        public async Task<IActionResult> Index()
        {
            var model = _mapper.Map<List<UploadViewModel>>(await _uploadService.GetAll());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "Uploads");
            await _uploadService.Remove(id, uploadsRootFolder);
            return RedirectToAction(nameof(Index));
        }

        // TODO details
    }
}
