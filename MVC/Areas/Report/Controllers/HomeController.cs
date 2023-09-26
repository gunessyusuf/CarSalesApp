using Business.Models.Report;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Areas.Report.Models;

namespace MVC.Areas.Report.Controllers
{
    [Area("Report")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IBrandService _brandService;

        public HomeController(IReportService reportService, IBrandService brandService)

        {
            _reportService = reportService;
            _brandService = brandService;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            var brandList = _brandService.Query().ToList();
            var brandSelectList = new SelectList(brandList, "Id", "Name");

            var model = _reportService.GetList(false);
            var viewModel = new HomeIndexViewModel()
            {
                Report = model,
                BrandSelectList = brandSelectList
            };
            return View(viewModel);

            //viewModel.Report = _reportService.GetList(true, viewModel.Filter );

            //viewModel.BrandSelectList = new SelectList(_brandService.Query().ToList(), "Id", "Name");

            //return View(viewModel);
        }

        [HttpPost]

        public IActionResult Index(FilterModel filter)
        {
            var model = _reportService.GetList(false, filter);
            return PartialView("_Report", model);
        }
    }
}
