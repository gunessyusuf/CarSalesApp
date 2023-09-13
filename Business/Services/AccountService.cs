using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Enums;

namespace Business.Services
{
    public interface IAccountService 
    {
        Result Login(AccountLoginModel accountLoginModel, UserModel userResultModel);

		Result Register(AccountRegisterModel model);
	}

    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;

        public AccountService(IUserService userService)
        {
            _userService = userService;
        }

        public Result Login(AccountLoginModel accountLoginModel, UserModel userResultModel)
        {
           var user = _userService.Query().SingleOrDefault(u => u.UserName == accountLoginModel.UserName && u.Password == accountLoginModel.Password && u.IsActive);

            if (user == null)
            {
                return new ErrorResult("Invalid user name or password!");
            }

            userResultModel.UserName = accountLoginModel.UserName;
            userResultModel.Role.Name = user.Role.Name;

            return new SuccessResult();
        }

		public Result Register(AccountRegisterModel model)
		{
			UserModel userModel = new UserModel()
			{
				IsActive = true,
				Password = model.Password,
				RoleId = (int)Roles.User,
				UserName = model.UserName
			};

			return _userService.Add(userModel);
		}
	}
}
