#nullable disable
using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Brand : RecordBase
	{
		[Required]
		[StringLength(150)]
		public string Name { get; set; }

        public List<Model> Models { get; set; }

        public List<Vehicle> Vehicles { get; set; }
	}
}