using KanbanApp.Models;
using KantineApp.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.Services
{
    public class MemberService : BaseService
    {
        private readonly string _apiPath = "/api/Members";

        public async Task<Member[]> GetMembershipsByUser(int userId)
        {
            var path = $"{_apiPath}/user/{userId}";
            var result = await GetData<Member[]>(path);
            return result;
        }

        public async Task<Member> CreateMembership(Member member)
        {
            var result = await PostData(member, _apiPath);
            return result;
        }
    }
}
