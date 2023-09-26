#nullable disable
using AppCore.Records.Bases;
using DataAccess.Entities;
using DataAccess.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserDetailModel 
    {
        [Key]
        public int UserId { get; set; }

        //public User User { get; set; }


        public Sex Sex { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        [Required]
        [StringLength(750)]
        public string Address { get; set; }

        [DisplayName("Country")]
        public int? CountryId { get; set; }

       

        [DisplayName("City")]
        public int? CityId { get; set; }

        


        #region Entity Referans Özelliklerine Karşılık Kullanacağımız Özellikler
        public CountryModel Country { get; set; }



        public CityModel City { get; set; }
        #endregion
    }
}
