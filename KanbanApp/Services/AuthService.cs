using KanbanApp.Models;
using KantineApp.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.Services
{
    public class AuthService : BaseService
    {
        private string apiPath = "/api/auth";

        public async Task<User> Login(string username, string password)
        {
            var path = $"{apiPath}/{username}/{password}";
            var result = await GetData<LoginDTO>(path);
            await SecureStorage.SetAsync("token", result.Token);
            return result.User;
        }

        public async Task<bool> Login(string token)
        {
            var result = await GetData<bool>(apiPath);
            return result;
        }

        public async Task DeleteToken()
        {
            await DeleteData(apiPath);
        }

        private class LoginDTO
        {
            public User User { get; set; }
            public string Token { get; set; }
        }
    }
}
