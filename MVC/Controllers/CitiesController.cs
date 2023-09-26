using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    [Route("[controller]")] 
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [Route("[action]/{countryId?}")] 
        public IActionResult GetCities(int? countryId) 
        {
            if (!countryId.HasValue)
                return NotFound();
            var cities = _cityService.GetList(countryId.Value);
            return Json(cities); 
        }
    }
}
