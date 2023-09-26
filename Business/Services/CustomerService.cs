using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
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
            if(_customerRepo.Exists(m => m.Name.ToLower() == model.Name.ToLower() && m.Surname.ToLower() == model.Surname.ToLower() && m.CustomerDetailId == model.CustomerDetailId))
            {
               return new ErrorResult("Customer is exists!");
            }
            var entity = new Customer()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                CustomerDetailId = model.CustomerDetailId,
                Notes = model.Notes,
                CustomerDetail = model.CustomerDetail

            };

            _customerRepo.Add(entity);

            return new SuccessResult("Customer Added Successfully");
        }

        public Result Delete(int id)
        {
            var entity = _customerRepo.Query().SingleOrDefault(c => c.Id == id);

            _customerRepo.Delete(entity);

            return new SuccessResult("Customer Deleted Successfully.");
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
                CustomerDetailId = c.CustomerDetailId.Value,
                Notes = c.Notes,
                CustomerDetail = c.CustomerDetail,
               
                CustomerDisplay = c.Name + " " + c.Surname,

                CarsDisplay = string.Join("<br />", c.Vehicles.Select(c => c.Brand))
            });
        }

        public Result Update(CustomerModel model)
        {
			if (_customerRepo.Exists(m => m.Name.ToLower() == model.Name.ToLower() && m.Surname.ToLower() == model.Surname.ToLower() && m.CustomerDetailId == model.CustomerDetailId && m.Id != model.Id))
			{
				return new ErrorResult("Customer is exists!");
			}
			var entity = _customerRepo.Query().SingleOrDefault(b => b.Id == model.Id);

            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Surname = model.Surname;
            entity.CustomerDetailId = model.CustomerDetailId;
            entity.Notes = model.Notes;
            entity.CustomerDetail = model.CustomerDetail;


            _customerRepo.Update(entity);

            return new SuccessResult("Customer Updated Successfully!");
        }
    }
}
