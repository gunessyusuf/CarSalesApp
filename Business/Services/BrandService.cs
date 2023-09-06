using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
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
			throw new NotImplementedException();
		}

		public Result Delete(int id)
		{
			throw new NotImplementedException();
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
				Name = b.Name
			});

		}

		public Result Update(BrandModel model)
		{
			throw new NotImplementedException();
		}
	}
}
