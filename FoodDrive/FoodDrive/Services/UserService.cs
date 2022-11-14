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
        // Using json-server in development mode
        //public List<User> Users { get; private set; }
        public static Login LoginInfo { get; private set; }

        public UserService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        // Using json-server in development mode
        public bool UserExists(List<User> users, string email, string password)
        {
            return users.Any<User>(u => (u.Email == email && u.Password == password));
        }
        public async Task<bool> Login(string email, string password)
        {
            Uri uri = new Uri(string.Format(UserContext.RestUrl, string.Empty));
            // Using json-server in development mode
            //Users = new List<User>();
            LoginInfo = new Login();
            try
            {
                // Using json-server in development mode
                //HttpResponseMessage response = await client.GetAsync(uri);
                //if (response.IsSuccessStatusCode)
                //{
                //    string content = await response.Content.ReadAsStringAsync();
                //    // Json format
                //    //Users = JsonSerializer.Deserialize<List<User>>(content, serializerOptions);
                //    // Bson format
                //    Users = BsonSerializer.Deserialize<List<User>>(content);
                //}
                // Using aws in production
                string jsonData = JsonSerializer.Serialize( new { email = email, password = password });
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    LoginInfo = BsonSerializer.Deserialize<Login>(result);
                    //Console.WriteLine(LoginInfo);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            // Using json-server in development mode
            //if (UserExists(Users, email, password))
            //{
            //    return true;
            //}
            // Using aws in production
            if (LoginInfo.success) return true;
            return false;
        }
        public string GetToken()
        {
            return LoginInfo.token.ToString();
        }

        public string GetUserId()
        {
            return LoginInfo.userInfo.Id.ToString();
        }

        public Task Logout()
        {
            LoginInfo = null; return Task.CompletedTask;
        }
    }
}
