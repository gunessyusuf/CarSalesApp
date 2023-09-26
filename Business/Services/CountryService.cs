using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{
    public interface ICountryService : IService<CountryModel>
    {
        List<CountryModel> GetList(); 

        CountryModel GetItem(int id); 
    }

    public class CountryService : ICountryService
    {
        private readonly RepoBase<Country> _countryRepo;

        public CountryService(RepoBase<Country> countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public Result Add(CountryModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _countryRepo.Dispose();
        }

        public List<CountryModel> GetList()
        {
            return Query().ToList();
        }

        public CountryModel GetItem(int id)
        {
            return Query().SingleOrDefault(c => c.Id == id);
        }

        public IQueryable<CountryModel> Query()
        {
            return _countryRepo.Query().OrderBy(c => c.Name).Select(c => new CountryModel
            {
                Name = c.Name,                
                Id = c.Id
            });
        }

        public Result Update(CountryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
