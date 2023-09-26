
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Business.Services
{
    public interface IUserService : IService<UserModel>
    {

    }

    public class UserService : IUserService
    {
        private readonly RepoBase<User> _userRepo;

        public UserService(RepoBase<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public Result Add(UserModel model)
        {
            List<User> users = _userRepo.GetList();
            if (users.Any(u => u.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase)))
                return new ErrorResult("Users with same name exits!");

            User entity = new User()
            {
                IsActive = model.IsActive,
                UserName = model.UserName,
                RoleId = model.RoleId,
                Password = model.Password
            };

            _userRepo.Add(entity);

            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _userRepo.Dispose();
        }

        public IQueryable<UserModel> Query()
        {
            return _userRepo.Query().OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName)
                .Select(u => new UserModel()
                {
                    UserName = u.UserName,
                    Password = u.Password,
                    IsActive = u.IsActive,
                    RoleId = u.RoleId,
                    Id = u.Id,
                    Role = new RoleModel()
                    {
                        Name = u.Role.Name
                    }
                });
        }

        public Result Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
