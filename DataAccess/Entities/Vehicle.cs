﻿#nullable disable
using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	public class Vehicle : RecordBase
	{
		[Required]
		
		public double Price { get; set; }

		[Required]
		
		public int Year { get; set; }

		public string Description { get; set; }

        public bool IsSold { get; set; }

		[Required]
		public int BrandId { get; set; }

		public Brand Brand { get; set; }

		[Required]
		public int ColorId { get; set; }

        public Color Color { get; set; }

		[Required]
		public int ModelId { get; set; }

		public Model Model { get; set; }

		public int? CustomerId { get; set; }

		public Customer Customer { get; set; }

		#region Binary Data
		[Column(TypeName = "image")]
		public byte[] Image { get; set; }

		[StringLength(5)]
		public string ImageExtension { get; set; } 
		#endregion

	}
}
