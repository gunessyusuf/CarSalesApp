using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;
using MVC.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Claims;

namespace MVC.Controllers
{
	public class FavoritesController : Controller
	{
		const string SESSIONKEY = "favorites";

		int _userId;

		private readonly IVehicleService _vehicleService;

		public FavoritesController(IVehicleService vehicleService)
		{
			_vehicleService = vehicleService;
		}

		public IActionResult GetFavorites()
		{
			_userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);

			var favoritesList = GetSession(_userId);
			return View("Favorites", favoritesList);
		}

		public IActionResult AddToFavorites(int vehicleId)
		{
			_userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);

			var favoritesList = GetSession(_userId);

			var vehicle = _vehicleService.Query().SingleOrDefault(v => v.Id == vehicleId);

			if (favoritesList.Any(f => f.VehicleId == vehicleId && f.UserId == _userId))
			{
				TempData["Message"] = $"\"{vehicle.Brand.Name} {vehicle.Model.Name}\" already added to favorites.";
			}

			else
			{
				var favoritesItem = new FavoritesModel()
				{
					VehicleId = vehicleId,
					UserId = _userId,
					BrandDisplay = vehicle.Brand.Name,
					ModelDisplay = vehicle.Model.Name,
					PriceDisplay = vehicle.Price.ToString("C2", new CultureInfo("en-US"))

				};
				favoritesList.Add(favoritesItem);

				SetSession(favoritesList);

				TempData["Message"] = $"\"{vehicle?.Brand.Name} {vehicle?.Model.Name}\" added to favorites.";
			}
				return RedirectToAction("Index", "Vehicles");
			
		}

		public IActionResult RemoveFromFavorites(int vehicleId, int userId)
		{
			_userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);

			var favoritesList = GetSession(_userId);

			
			favoritesList.RemoveAll(f => f.VehicleId == vehicleId && f.UserId == userId);

			SetSession(favoritesList);

			return RedirectToAction(nameof(GetFavorites));
		}

		public IActionResult ClearFavorites()
		{
			_userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);

			var favoritesList = GetSession(_userId);

			favoritesList.RemoveAll(f => f.UserId == _userId);

			SetSession(favoritesList);

			return RedirectToAction(nameof(GetFavorites));
		}

		private List<FavoritesModel> GetSession(int userId)
		{
			var favoritesList = new List<FavoritesModel>();

			var favoritesJson = HttpContext.Session.GetString(SESSIONKEY);

			if (!string.IsNullOrWhiteSpace(favoritesJson))
			{
				favoritesList = JsonConvert.DeserializeObject<List<FavoritesModel>>(favoritesJson);

				favoritesList = favoritesList.Where(f => f.UserId == userId).ToList();


			}

			return favoritesList;
		}

		private void SetSession(List<FavoritesModel> favoritesList)
		{
			var favoritesJson = JsonConvert.SerializeObject(favoritesList);

			HttpContext.Session.SetString(SESSIONKEY, favoritesJson);
		}
	}
}
