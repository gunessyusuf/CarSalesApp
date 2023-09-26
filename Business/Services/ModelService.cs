using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Business.Services
{
	public interface IModelService : IService<ModelModel>
	{

	}

	public class ModelService : IModelService
	{
		private readonly RepoBase<Model> _modelRepo;

		public ModelService(RepoBase<Model> modelRepo)
		{
			_modelRepo = modelRepo;
		}

		public Result Add(ModelModel model)
		{
			if (_modelRepo.Exists(m => m.Name.ToLower() == model.Name.ToLower() && m.BrandId == model.BrandId))
			{
				return new ErrorResult("Model is exists.");
			}
			var entity = new Model()
			{
				Brand = model.Brand,
				BrandId = model.BrandId,
				Id = model.Id,
			    Name = model.Name,
				
			};

			_modelRepo.Add(entity);
			return new SuccessResult("Model Added Successfully");
		}

		public Result Delete(int id)
		{
			var entity = _modelRepo.Query().SingleOrDefault(m => m.Id == id);
			_modelRepo.Delete(entity);
			return new SuccessResult("Model Deleted Successfully");
		}

		public void Dispose()
		{
			_modelRepo.Dispose();
		}

		public IQueryable<ModelModel> Query()
		{
			return _modelRepo.Query().OrderBy(m => m.Name).Select(m => new ModelModel()
			{
				Id = m.Id,
				Name = m.Name,
				BrandId = m.BrandId,
				Brand = m.Brand,
			});
		}

		public Result Update(ModelModel model)
		{
            if (_modelRepo.Exists(m => m.Name.ToLower() == model.Name.ToLower() && m.BrandId == model.BrandId && m.Id != model.Id))
            {
                return new ErrorResult("Model is exists.");
            }

            var entity = new Model()
            {
                Brand = model.Brand,
                BrandId = model.BrandId,
                Id = model.Id,
                Name = model.Name,

            };

           _modelRepo.Update(entity);
            return new SuccessResult("Model Updated Successfully");
        }
	}
}
