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
		private readonly RepoBase<Model> _modelRepo;
        public BrandService(RepoBase<Brand> brandRepo, RepoBase<Model> modelRepo)
        {
            _brandRepo = brandRepo;
            _modelRepo = modelRepo;
        }

        public Result Add(BrandModel model)
		{
			if(_brandRepo.Exists(b => b.Name.ToLower() == model.Name.ToLower().Trim()))
			{
				return new ErrorResult("Brand is exits!");
			}
			var entity = new Brand()
			{
				Id = model.Id,
				Name = model.Name,
			   
			};

			_brandRepo.Add(entity);
			return new SuccessResult("Brand Added Successfully");
		}

		public Result Delete(int id)
		{		

			var entity = Query().SingleOrDefault(b => b.Id == id);
			if (entity.ModelsCount > 0)
			{
				return new ErrorResult("Brand can not be deleted because brand has models");
			}


			_brandRepo.Delete(b => b.Id == id);
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
				ModelsCount = b.Models.Count,

				ModelsDisplay = string.Join("<br />", b.Models.Select(b => b.Name)),
			});

		}

		public Result Update(BrandModel model)
		{
            if (_brandRepo.Exists(b => b.Name.ToLower() == model.Name.ToLower().Trim() && b.Id != model.Id))
            {
               return new ErrorResult("Brand is exits!");
            }

            var entity = new Brand()
            {
                Id = model.Id,
                Name = model.Name,

            };

            _brandRepo.Update(entity);
            return new SuccessResult("Brand Updated Successfully");
        }
	}
}
