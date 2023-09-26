#nullable disable
using AppCore.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class ModelModel : RecordBase
	{
		[Required]
		[StringLength(500)]
		public string Name { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }
    }
}
