#nullable disable
using AppCore.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class CountryModel : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<City> Cities { get; set; }

        public List<UserDetail> UserDetails { get; set; }
        public List<CustomerDetail> CustomersDetails { get; set; }
    }
}
