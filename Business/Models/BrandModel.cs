#nullable disable
using AppCore.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models
{
	public class BrandModel : RecordBase
	{
		[Required]
		[StringLength(150)]
		public string Name { get; set; }

		public List<Model> Models { get; set; }

		public List<Vehicle> Vehicles { get; set; }


		[NotMapped]
		[DisplayName("Models")]
        public string ModelsDisplay { get; set; }

		[NotMapped]
		[DisplayName("Model Count")]
        public int ModelsCount { get; set; }
    }
}
