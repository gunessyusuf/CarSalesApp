#nullable disable
using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	public class Customer : RecordBase
	{
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Surname { get; set; }
        public int CustomerDetailId { get; set; }

		public CustomerDetail CustomerDetail { get; set; }

		public string Notes { get; set; }

        public List<Vehicle> Vehicles { get; set; }


        public string FullName  => $"{Name} {Surname} ";

    }
}
