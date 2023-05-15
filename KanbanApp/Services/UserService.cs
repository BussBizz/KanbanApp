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

        public async Task<User> CreateUser(Password password)
        {
            var result = await PostData(password, apiPath);
            return result.User;
        }

    }
}
