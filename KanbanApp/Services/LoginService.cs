using KanbanApp.Models;
using KantineApp.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.Services
{
    public class LoginService : BaseService
    {
        private string apiPath = "/api/Login";

        public async Task<User> Login(string username)
        {
            var path = $"{apiPath}/{username}";
            var result = await GetData<User>(path);
            return result;
        }

        public async Task<bool> CheckUserName(string username)
        {
            var path = $"{apiPath}/{username}/check";
            var result = await GetData<bool>(path);
            return result;
        }
    }
}
