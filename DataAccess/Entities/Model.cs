#nullable disable
using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Model : RecordBase
	{
		[Required]
		[StringLength(150)]
		public string Name { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
}
