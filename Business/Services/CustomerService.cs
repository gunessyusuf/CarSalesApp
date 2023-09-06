using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Business.Services
{
	public interface ICustomerService : IService<CustomerModel>
	{

	}

	public class CustomerService : ICustomerService
	{
		private readonly RepoBase<Customer> _customerRepo;

		public CustomerService(RepoBase<Customer> customerRepo)
		{
			_customerRepo = customerRepo;
		}

		public Result Add(CustomerModel model)
		{
			throw new NotImplementedException();
		}

		public Result Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_customerRepo.Dispose();
		}

		public IQueryable<CustomerModel> Query()
		{
			return _customerRepo.Query().OrderBy(c => c.Name).Select(c => new CustomerModel()
			{
				Id = c.Id,
				Name = c.Name,
				Surname = c.Surname,

				CustomerDisplay = c.Name + " " + c.Surname,

				CarsDisplay = string.Join("<br />", c.Vehicles.Select(c => c.Brand))
			});
		}

		public Result Update(CustomerModel model)
		{
			throw new NotImplementedException();
		}
	}
}
