using Microsoft.AspNetCore.Mvc;

namespace SmartComputer_Admin.Controllers.Master
{
    public class BrandController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment Environment;
        public BrandController(IWebHostEnvironment environment)
        {
            //_httpContextAccessor = httpContextAccessor;
            Environment = environment;


        }
        public IActionResult Index()
        {
            return View("Views/Master/Brand.cshtml");
        }
    }
}
