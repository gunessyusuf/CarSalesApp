#nullable disable
using DataAccess.Entities;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MVC.Models
{
	public class FavoritesModel
	{
		

		public int VehicleId { get; set; }

        public int UserId { get; set; }		
		

		[DisplayName("Brand")]
        public string BrandDisplay { get; set; }

		[DisplayName("Model")]
        public  string ModelDisplay { get; set; }

		[DisplayName("Price")]
		public string PriceDisplay { get; set; }
	}
}
