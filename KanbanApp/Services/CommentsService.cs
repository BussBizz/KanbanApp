using KanbanApp.Models;
using KantineApp.BL.Services;

namespace KanbanApp.Services
{
    public class CommentsService : BaseService
    {
        private string _apiPath = "/api/Comments";

        public async Task<Comment[]> GetCommentsByTask(int id)
        {
            var path = $"{_apiPath}/task/{id}";
            var result = await GetData<Comment[]>(path);
            return result;
        }

        public async Task<Comment> PostComment(Comment comment)
        {
            var result = await PostData(comment, _apiPath);
            return result;
        }
    }
}
