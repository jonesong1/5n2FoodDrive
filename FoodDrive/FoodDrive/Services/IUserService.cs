using FoodDrive.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodDrive.Services
{
    public interface IUserService
    {
        bool UserExists(List<User> users, string email, string password);
        Task<bool> Login(string email, string password);
    }
}
