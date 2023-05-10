using KanbanApp.Models;
using KantineApp.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.Services
{
    public class UserService : BaseService
    {
        private string apiPath = "/api/users";

        public async Task<bool> CheckUserName(string username)
        {
            // TODO fix when webAPI is updated
            //var path = $"{apiPath}/{username}";
            //var result = await GetData<bool>(path);
            //return result;

            var rnd = new Random().Next(1, 10);

            return rnd < 5;
        }

        public async Task<User> CreateUser(User user)
        {
            var result = await PostData(user, apiPath);
            return result;
        }

    }
}
