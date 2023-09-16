using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Sale.Controllers
{
    [Area("Sale")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ISaleService _saleService;

        public HomeController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            var model = _saleService.GetList();

            return View(model);
        }
    }
}
