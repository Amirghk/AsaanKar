using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FinalProject.Endpoint.Areas.Administration.Pages.SeedData
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<IndexModel> _logger;
        private readonly ICityService _cityService;
        private readonly IProvinceService _provinceService;
        private readonly ICategoryService _categoryService;
        private readonly IServiceService _serviceService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<IndexModel> logger,
            ICityService cityService,
            IProvinceService provinceService,
            ICategoryService categoryService,
            IServiceService serviceService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
            _cityService = cityService;
            _provinceService = provinceService;
            _categoryService = categoryService;
            _serviceService = serviceService;
            _emailStore = GetEmailStore();
        }
        public async Task OnGetAsync()
        {
            //var user = Activator.CreateInstance<ApplicationUser>();

            //await _userStore.SetUserNameAsync(user, "admin@gmail.com", CancellationToken.None);
            //await _emailStore.SetEmailAsync(user, "admin@gmail.com", CancellationToken.None);
            //var result = await _userManager.CreateAsync(user, "Asdasdasd");

            //if (result.Succeeded)
            //{
            //    // Add related data to claims
            //    var adminClaim = new Claim("IsAdmin", true.ToString());
            //    await _userManager.AddClaimAsync(user, adminClaim);

            //    var nameClaim = new Claim(ClaimTypes.Name, "Admin");
            //    await _userManager.AddClaimAsync(user, nameClaim);
            //}
            //var provinceId = await _provinceService.Set(new ProvinceDto
            //{
            //    IsSupported = true,
            //    Name = "Tehran"
            //});
            //await _cityService.Set(new CityDto
            //{
            //    IsSupported = true,
            //    Name = "Tehran",
            //    ProvinceId = provinceId
            //});

            var cId = await _categoryService.Set(new CategoryDto
            {
                Name = "Electronics2",
                ParentCategoryId = null,
                Description = "Something"
            });
            var cIdd = await _categoryService.Set(new CategoryDto
            {
                Name = "Mobile Services",
                ParentCategoryId = cId,
                Description = "Fix your phone"
            });
            await _serviceService.Set(new ServiceDto
            {
                Description = "battery service",
                CategoryId = cIdd,
                BasePrice = 1000000
            });
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
