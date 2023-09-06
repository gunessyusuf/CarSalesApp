using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{
	public interface IBrandService : IService<BrandModel>
	{

	}

	public class BrandService : IBrandService
	{
		private readonly RepoBase<Brand> _brandRepo;

		public BrandService(RepoBase<Brand> brandRepo)
		{
			_brandRepo = brandRepo;
		}

		public Result Add(BrandModel model)
		{
			var entity = new Brand()
			{
				Id = model.Id,
				Name = model.Name,
			    Models = model.Models,
			    Vehicles = model.Vehicles,
			};

			_brandRepo.Add(entity);
			return new SuccessResult("Entity Added Successfully");
		}

		public Result Delete(int id)
		{
			var entity = _brandRepo.Query().SingleOrDefault(b => b.Id == id);
			_brandRepo.Delete(entity);
			return new SuccessResult("Brand Deleted Successfully");
		}

		public void Dispose()
		{
			_brandRepo.Dispose();
		}

		public IQueryable<BrandModel> Query()
		{
			return _brandRepo.Query().OrderBy(b => b.Name).Select(b => new BrandModel()
			{
				Id = b.Id,
				Name = b.Name,

				ModelsDisplay = string.Join("<br />", b.Models.Select(b =>b.Name)),		
				
				
			});

		}

		public Result Update(BrandModel model)
		{
            var entity = new Brand()
            {
                Id = model.Id,
                Name = model.Name,
                Models = model.Models,
                Vehicles = model.Vehicles,
            };

            _brandRepo.Update(entity);
            return new SuccessResult("Entity Updated Successfully");
        }
	}
}
