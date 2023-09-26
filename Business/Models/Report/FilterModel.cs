#nullable disable

using DataAccess.Entities;
using System.ComponentModel;

namespace Business.Models.Report
{
    public class FilterModel
    {
        [DisplayName("Brand")]
        public int? BrandId { get; set; }

        

        [DisplayName("Price")]
        public double? PriceBegin { get; set; }
        public double? PriceEnd { get; set; }

        [DisplayName("Year")]
        public int? YearBegin { get; set; }
        public int? YearEnd { get; set; }

        public bool IsSold { get; set; }
       

        [DisplayName("Sold")]
        public string SoldDisplay { get; set; }

    }
}
