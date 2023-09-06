#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Color : RecordBase
	{
		[Required]
		[StringLength(100)]
		public string Name { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
}
