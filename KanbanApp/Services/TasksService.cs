using KanbanApp.Models;
using KantineApp.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.Services
{
    public class TasksService : BaseService
    {
        private string _apiPath = "/api/KanbanTasks";
        public async Task<KanbanTask[]> GetTasks()
        {
            var result = await GetData<KanbanTask[]>(_apiPath);
            return result;
        }
        public async Task<KanbanTask> GetTask(int id)
        {
            var path = $"{_apiPath}/{id}";
            var result = await GetData<KanbanTask>(path);
            return result;
        }

        public async Task<KanbanTask> PostTask(KanbanTask kanbanTask)
        {
            var result = await PostData(kanbanTask, _apiPath);
            return result;
        }
    }
}
