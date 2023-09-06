#nullable disable
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Business.Services
{
	public interface IColorService : IService<ColorModel>
	{

	}

	public class ColorService : IColorService
	{
		private readonly RepoBase<Color> _colorRepo;

		public ColorService(RepoBase<Color> colorRepo)
		{
			_colorRepo = colorRepo;
		}

		public Result Add(ColorModel model)
		{
			throw new NotImplementedException();
		}

		public Result Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_colorRepo.Dispose();
		}

		public IQueryable<ColorModel> Query()
		{
			return _colorRepo.Query().OrderBy(c => c.Name).Select(c => new ColorModel()
			{
				Id = c.Id,
				Name = c.Name,
			});
		}

		public Result Update(ColorModel model)
		{
			throw new NotImplementedException();
		}
	}
}
