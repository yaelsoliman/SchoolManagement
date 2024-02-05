using SchoolManagement.Application.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interface.Service
{
    public interface IIdentityService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<string> CreateRoleAsync(string rolename);
        //Task<AuthModel> LogoutAsync();
    }
}
