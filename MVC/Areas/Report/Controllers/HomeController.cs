using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Report.Controllers
{
    [Area("Report")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;

        public HomeController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            var model = _reportService.GetList();

            return View(model);
        }       
    }
}
