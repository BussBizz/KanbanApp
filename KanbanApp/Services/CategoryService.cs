using KanbanApp.Models;
using KantineApp.BL.Services;

namespace KanbanApp.Services
{
    internal class CategoryService : BaseService
    {
        private string _apiPath = "/api/Categories";
        public async Task<Category[]> Getcategories()
        {
            var result = await GetData<Category[]>(_apiPath);
            return result;
        }
        public async Task<Category> GetCategory(int id)
        {
            var path = $"{_apiPath}/{id}";
            var result = await GetData<Category>(path);
            return result;
        }

        public async Task<Category> PostCategory(Category category)
        {
            var result = await PostData(category, _apiPath);
            return result;
        }

        public async Task<Category[]> GetCategoriesByBoard(int boardId)
        {
            var path = $"{_apiPath}/board/{boardId}";
            var result = await GetData<Category[]>(path);
            return result;
        }
    }
}
