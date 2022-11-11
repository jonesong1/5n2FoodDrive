using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodDrive.Services
{
    public class UserManager
    {
        IUserService userService;
        public UserManager(IUserService userService)
        {
            this.userService = userService;
        }
        public Task<bool> Login(string email, string password)
        {
            return userService.Login(email, password);
        }
    }
}
