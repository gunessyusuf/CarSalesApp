#nullable disable
using Business.Models.Report;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Areas.Report.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<ReportItemModel> Report { get; set; }
        public FilterModel Filter { get; set; }

        public SelectList BrandSelectList { get; set; }
    }
}
