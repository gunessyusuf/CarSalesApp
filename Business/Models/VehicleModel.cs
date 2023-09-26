#nullable disable
using AppCore.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models
{
	public class VehicleModel : RecordBase
    {
		[Range(100000, 1000000000, ErrorMessage = "{0} must be between {1} and {2}!")]
		public double Price { get; set; }



		[Range(1980, 2023, ErrorMessage = "{0} must be between {1} and {2}!")]
		public int Year { get; set; }

        public string Description { get; set; }

        
        public bool IsSold { get; set; }

		
		public int BrandId { get; set; }

		
		public Brand Brand { get; set; }

		
		public int ColorId { get; set; }

		
		public Color Color { get; set; }

		
		public int ModelId { get; set; }

		
		public Model Model { get; set; }

        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }

        [NotMapped]
        [DisplayName("Customer")]
        public string CustomerDisplay { get; set; }

		
		
		[NotMapped]
		
		[DisplayName("Price")]
		public string PriceDisplay { get; set; }

		[NotMapped]
        [DisplayName("Sold")]
        public string SoldDisplay { get; set; }

        public int? UserId { get; set; }


		#region Binary Data
		[Column(TypeName = "image")]
		public byte[] Image { get; set; }

		[StringLength(5)]
		public string ImageExtension { get; set; }

		[DisplayName("")]
		public string ImgSrcDisplay { get; set; }
		#endregion


	}
}
