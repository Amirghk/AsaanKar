using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Administration.Models;
using FinalProject.Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize("IsAdmin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentController> _logger;

        public CommentController(
            ICommentService commentService,
            IMapper mapper,
            ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var model = _mapper.Map<List<CommentListViewModel>>(await _commentService.GetAll(cancellationToken));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _commentService.Remove(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id, CancellationToken cancellationToken)
        {
            await _commentService.Approve(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
