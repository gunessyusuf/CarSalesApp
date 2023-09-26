﻿using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{
    public interface ICityService : IService<CityModel>
    {
        List<CityModel> GetList(int countryId); 
        List<CityModel> GetList();  
        CityModel GetItem(int id); 
    }
    public class CityService : ICityService
    {
        private readonly RepoBase<City> _cityRepo;

        public CityService(RepoBase<City> cityRepo)
        {
            _cityRepo = cityRepo;
        }

        public Result Add(CityModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _cityRepo.Dispose();
        }

        public CityModel GetItem(int id)
        {
            return Query().SingleOrDefault(c => c.Id == id);
        }

        public List<CityModel> GetList(int countryId)
        {
            return Query().Where(c => c.CountryId == countryId).ToList();
        }

        public List<CityModel> GetList()
        {
            return Query().ToList();
        }

        public IQueryable<CityModel> Query()
        {
            return _cityRepo.Query().OrderBy(c => c.Name).Select(c => new CityModel()
            {
                CountryId = c.CountryId,
                Name = c.Name,               
                Id = c.Id
            });
        }

        public Result Update(CityModel model)
        {
            throw new NotImplementedException();
        }
    }
}
