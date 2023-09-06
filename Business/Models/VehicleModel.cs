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
        [Required]
        [StringLength(150)]
        public double Price { get; set; }

        [Required]
        [StringLength(10)]
        public int Year { get; set; }

        public string Description { get; set; }

        [DisplayName("Sold")]
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
    }
}
