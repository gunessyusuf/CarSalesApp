#nullable disable
using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class ModelModel : RecordBase
	{
		[Required]
		[StringLength(150)]
		public string Name { get; set; }
	}
}
