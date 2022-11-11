using FoodDrive.Context;
using FoodDrive.Models;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodDrive.Services
{
    public class UserService : IUserService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;

        public List<User> Users { get; private set; }

        public UserService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public bool UserExists(List<User> users, string email, string password)
        {
            return users.Any<User>(u => (u.Email == email && u.Password == password));
        }
        public async Task<bool> Login(string email, string password)
        {
            Uri uri = new Uri(string.Format(UserContext.RestUrl, string.Empty));
            Users = new List<User>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    // Json format
                    //Users = JsonSerializer.Deserialize<List<User>>(content, serializerOptions);
                    // Bson format
                    Users = BsonSerializer.Deserialize<List<User>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            if (UserExists(Users, email, password))
            {
                return true;
            }
            return false;
        }

        
    }
}
