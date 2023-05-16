using KanbanApp.Models;
using KantineApp.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.Services
{
    public class InviteService : BaseService
    {
        private string _apiPath = "/api/Invites";
        private Random _random = new Random();

        public async Task<Invite> CreateAnonInvite(Board board)
        {
            var invite = new Invite
            {
                BoardId = board.Id,
                Expire = DateTime.Now.AddDays(1),
                Code = RandomString(),
            };

            var result = await PostData(invite, _apiPath);
            return result;
        }

        public async Task<Invite> CreateUserInvite(Board board, User user)
        {
            var invite = new Invite
            {
                BoardId = board.Id,
                UserId = user.Id,
                Expire = DateTime.Now.AddDays(2),
            };

            var result = await PostData(invite, _apiPath);
            return result;
        }

        public async Task<Invite[]> GetInvitesByBoard(int boardId)
        {
            var path = $"{_apiPath}/board/{boardId}";
            var result = await GetData<Invite[]>(path);
            return result;
        }

        public async Task<Invite[]> GetInvitesByUser(int userId)
        {
            var path = $"{_apiPath}/user/{userId}";
            var result = await GetData<Invite[]>(path);
            return result;
        }

        public async Task<Invite> GetInviteByCode(string code)
        {
            var path = $"{_apiPath}/code/{code}";
            var result = await GetData<Invite>(path);
            return result;
        }

        public async Task<Invite> DeleteInvite(Invite invite)
        {
            var path = $"{_apiPath}/{invite.Id}";
            var result = await DeleteData<Invite>(path);
            return result;
        }

        private string RandomString(int length = 6)
        {
            const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var r = _random.Next(0, pool.Length);
                var c = pool[r];
                builder.Append(c);
            }

            return builder.ToString();
        }
    }
}
