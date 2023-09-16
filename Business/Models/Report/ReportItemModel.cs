#nullable disable
using System.ComponentModel;

namespace Business.Models.Report
{
    public class ReportItemModel
    {
        [DisplayName("Vehicle Brand")]
        public string VehicleBrand { get; set; }

        [DisplayName("Vehicle Model")]
        public string VehicleModel { get; set; }

        [DisplayName("Vehicle Color")]
        public string VehicleColor { get; set; }

        [DisplayName("Vehicle Price")]
        public string VehiclePrice { get; set; }

        [DisplayName("Vehicle Year")]
        public int VehicleYear { get; set; }

        [DisplayName("Sold")]
        public string IsSold { get; set; }

        public string Customer { get; set; }


    }
}
