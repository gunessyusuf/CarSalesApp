﻿#nullable disable
using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models
{
	public class CustomerModel : RecordBase
	{
		[Required]
		[StringLength(150)]
		public string Name { get; set; }

		[Required]
		[StringLength(150)]
		public string Surname { get; set; }

		[NotMapped]
		[DisplayName("Cars")]
        public string CarsDisplay { get; set; }

		[NotMapped]
		[DisplayName("Customer")]
		public string CustomerDisplay { get; set; }
	}
}
