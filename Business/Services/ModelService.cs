using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
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
			throw new NotImplementedException();
		}

		public Result Delete(int id)
		{
			throw new NotImplementedException();
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
			});
		}

		public Result Update(ModelModel model)
		{
			throw new NotImplementedException();
		}
	}
}
