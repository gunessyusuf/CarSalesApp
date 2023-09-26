using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models.Report;
using DataAccess.Entities;
using Microsoft.Identity.Client;
using System.Globalization;

namespace Business.Services
{
    public interface IReportService
    {
        List<ReportItemModel> GetList(bool useInnerJoin = true, FilterModel filter = null);
    }

    public class ReportService : IReportService
    {
        private readonly RepoBase<Vehicle> _vehicleRepo;

        public ReportService(RepoBase<Vehicle> vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
        }



        public List<ReportItemModel> GetList(bool useInnerJoin = true, FilterModel filter = null)
        {
            #region Sorgu Oluşturma
            var vehicleQuery = _vehicleRepo.Query();
            var brandQuery = _vehicleRepo.Query<Brand>();
            var modelQuery = _vehicleRepo.Query<Model>();
            var colorQuery = _vehicleRepo.Query<Color>();
            var customerQuery = _vehicleRepo.Query<Customer>();


            IQueryable<ReportItemModel> query;
           if(useInnerJoin)
            query = from v in vehicleQuery
                    join b in brandQuery
                    on v.BrandId equals b.Id
                    join m in modelQuery
                    on v.ModelId equals m.Id
                    join c in colorQuery
                    on v.ColorId equals c.Id
                    //join cs in customerQuery
                    //on v.CustomerId equals cs.Id
                    select new ReportItemModel()
                    {
                        BrandId = v.BrandId,
                        VehicleBrand = b.Name,
                        VehicleModel = m.Name,
                        VehicleColor = c.Name,
                        VehiclePrice = v.Price,
                        VehicleYear = v.Year,
                        Customer = v.Customer.Name + " " + v.Customer.Surname,
                        IsSold = v.IsSold ? "Yes" : "No"
                    };
           else
            {
                query = from v in vehicleQuery
                        join b in brandQuery
                        on v.BrandId equals b.Id
                        join m in modelQuery
                        on v.ModelId equals m.Id into modelGroup
                        from m in modelGroup.DefaultIfEmpty() // Left Outer Join için DefaultIfEmpty kullanılır
                        join c in colorQuery
                        on v.ColorId equals c.Id
                        select new ReportItemModel()
                        {
                            BrandId = v.BrandId,
                            VehicleBrand = b.Name,
                            VehicleModel = m != null ? m.Name : "No Model", // Sol tabloda eşleşen kayıt yoksa "No Model" döner
                            VehicleColor = c.Name,
                            VehiclePrice = v.Price,
                            VehicleYear = v.Year,
                            Customer = v.Customer.Name + " " + v.Customer.Surname,
                            IsSold = v.IsSold ? "Yes" : "No"
                        };
            }

            #endregion




            #region Sıralama
            //query = query.OrderByDescending(q => q.BlogCreateDate).ThenByDescending(q => q.BlogUpdateDate).ThenBy(q => q.BlogTitle);
            query = query.OrderBy(q => q.VehiclePrice);
            #endregion

            #region Filtreleme
            if (filter is not null)
            {
                if (filter.BrandId.HasValue)
                {
                    query = query.Where(q => q.BrandId ==  filter.BrandId);
                }
                if (filter.PriceBegin.HasValue)
                {
                    query = query.Where(q => q.VehiclePrice >= filter.PriceBegin.Value);
                }
                if (filter.PriceEnd.HasValue)
                {
                    query = query.Where(q => q.VehiclePrice <= filter.PriceEnd.Value);
                }

                if (filter.YearBegin.HasValue)
                {
                    query = query.Where(q => q.VehicleYear >= filter.YearBegin.Value);
                }
                if (filter.YearEnd.HasValue)
                {
                    query = query.Where(q => q.VehicleYear <= filter.YearEnd.Value);
                }
            }
            #endregion

            return query.ToList();


        }
    }
}
