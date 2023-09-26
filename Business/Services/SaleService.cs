using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models.Report;
using DataAccess.Entities;
using System.Globalization;

namespace Business.Services
{
    public interface ISaleService
    {
        List<ReportItemModel> GetList();
    }

    public class SaleService : ISaleService
    {
        private readonly RepoBase<Vehicle> _vehicleRepo;

        public SaleService(RepoBase<Vehicle> vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
        }
        public List<ReportItemModel> GetList()
        {
            #region Sorgu Oluşturma
            var vehicleQuery = _vehicleRepo.Query();
            var brandQuery = _vehicleRepo.Query<Brand>();
            var modelQuery = _vehicleRepo.Query<Model>();
            var colorQuery = _vehicleRepo.Query<Color>();
            var customerQuery = _vehicleRepo.Query<Customer>();


            IQueryable<ReportItemModel> query;

            query = from v in vehicleQuery
                    join b in brandQuery
                    on v.BrandId equals b.Id
                    join m in modelQuery
                    on v.ModelId equals m.Id
                    join c in colorQuery
                    on v.ColorId equals c.Id
                    join cs in customerQuery
                    on v.CustomerId equals cs.Id
                    where v.IsSold
                    orderby v.Customer.Name descending 
                    orderby v.Customer.Surname descending
                    
                    select new ReportItemModel()
                    {
                        VehicleBrand = b.Name,
                        VehicleModel = m.Name,
                        VehicleColor = c.Name,
                        VehiclePrice = v.Price,
                        VehicleYear = v.Year,
                        Customer = v.Customer.Name + " " + v.Customer.Surname,
                        IsSold = v.IsSold ? "Yes" : "No"
                    };
            #endregion

            return query.ToList();
        }
    }
}
