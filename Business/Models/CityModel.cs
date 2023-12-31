﻿#nullable disable
using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Business.Models
{
    public class CityModel : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(150, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("City Name")]
        public string Name { get; set; }

        [DisplayName("Country")]
        public int CountryId { get; set; }
    }
}
