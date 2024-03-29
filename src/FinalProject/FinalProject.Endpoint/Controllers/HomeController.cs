﻿using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Enums;
using FinalProject.Endpoint.Common.Filters;
using FinalProject.Endpoint.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IUploadService _uploadService;
        private readonly IServiceService _serviceService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IMapper mapper,
            ICategoryService categoryService,
            IUploadService uploadService,
            ILogger<HomeController> logger,
            IServiceService serviceService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _uploadService = uploadService;
            _serviceService = serviceService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            _logger.LogTrace("Start of {method}", nameof(Index));
            var model = _mapper.Map<List<CategoryViewModel>>(await _categoryService.GetAll(cancellationToken));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Category(int id, CancellationToken cancellationToken)
        {
            var categories = _mapper.Map<List<CategoryViewModel>>(await _categoryService.GetChildren(id, cancellationToken));
            return View(categories);
        }

        public IActionResult Error(ErrorTypes type)
        {
            string message = type switch
            {
                ErrorTypes.NotFound => "دیتایی با این شناسه موجود نمیباشد",
                ErrorTypes.UnfinishedOrder => "سفارش مرتبط با این سرویس تمام نشده",
                _ => "خطایی رخ داده است!"
            };

            return View("Error", message);
        }

    }
}
