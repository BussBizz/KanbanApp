using KanbanApp.Models;
using KantineApp.BL.Services;

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

        public async Task<Member[]> GetMembershipsByBoard(int boardId)
        {
            var path = $"{_apiPath}/board/{boardId}";
            var result = await GetData<Member[]>(path);
            return result;
        }

        public async Task<Member> CreateMembership(Member member)
        {
            var result = await PostData(member, _apiPath);
            return result;
        }

        public async Task<Member> MembershipFromInvite(Invite invite)
        {
            var member = new Member
            {
                BoardId = invite.BoardId,
                UserId = invite.UserId.Value,
            };
            var result = await PostData(member, _apiPath);
            result.Board = invite.Board;
            return result;
        }
    }
}
