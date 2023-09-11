using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using System.Globalization;
using System.Security.Cryptography.Xml;

namespace Business.Services
{
    public interface IVehicleService : IService<VehicleModel>
    {

    }

    public class VehicleService : IVehicleService
    {
        private readonly RepoBase<Vehicle> _vehicleRepo;

        public VehicleService(RepoBase<Vehicle> vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
        }

        public Result Add(VehicleModel model)
        {
			var entity = new Vehicle()
			{
				Id = model.Id,
				Price = model.Price,
				Brand = model.Brand,
				BrandId = model.BrandId,
				Color = model.Color,
				ColorId = model.ColorId,
				Year = model.Year,
				Customer = model.Customer,
				CustomerId = model.CustomerId,
				Description = model.Description,
				IsSold = model.IsSold,
				Model = model.Model,
				ModelId = model.ModelId,
                

			};           

            _vehicleRepo.Add(entity);
            return new SuccessResult("Vehicle Added Succeffully");
		}

        public Result Delete(int id)
        {
            var entity = _vehicleRepo.Query().SingleOrDefault(v => v.Id == id);

            _vehicleRepo.Delete(entity);

            return new SuccessResult("Vehicle Deleted Successfully");
        }

        public void Dispose()
        {
            _vehicleRepo.Dispose();
        }

        public IQueryable<VehicleModel> Query()
        {
           return _vehicleRepo.Query().OrderByDescending(v => v.Price).ThenBy(v => v.Year).Select(v => new VehicleModel
            {
               Id = v.Id,
               Price = v.Price,
               Brand = v.Brand,
               BrandId = v.BrandId,
               Color = v.Color,
               ColorId = v.ColorId,
               Year = v.Year,
               IsSold = v.IsSold,
               Model = v.Model,
               
               CustomerDisplay = v.Customer.Name + " " + v.Customer.Surname,

               PriceDisplay = v.Price.ToString("C2", new CultureInfo("en-US"))

               
               
            });

            
        }

        public Result Update(VehicleModel model)
        {
            var entity = new Vehicle()
            {
                Id = model.Id,
                Price = model.Price,
                Brand = model.Brand,
                BrandId = model.BrandId,
                Color = model.Color,
                ColorId = model.ColorId,
                Year = model.Year,
                Customer = model.Customer,
                CustomerId = model.CustomerId,
                Description = model.Description,
                IsSold = model.IsSold,
                Model = model.Model,
                ModelId = model.ModelId,


            };






            _vehicleRepo.Update(entity);

            return new SuccessResult("Vehicle Updated Successfully!");
        }
    }
}
