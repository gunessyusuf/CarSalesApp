#nullable disable
using AppCore.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserModel : RecordBase
    {
        [Required]
        [StringLength(15)]
        public string UserName { get; set; }

        [Required]
        [StringLength(15)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }
        

        public int? UserDetailId { get; set; }

        public UserDetailModel UserDetail { get; set; }

        public RoleModel Role { get; set; }

        public UserModel(string userName, string password, bool isActive, int roleId, int id)
        {
            UserName = userName;
            Password = password;
            IsActive = isActive;
            RoleId = roleId;
            Id = id;

            Role = new RoleModel();
        }

        public UserModel()
        {
            Role = new RoleModel();
        }
    }
}
