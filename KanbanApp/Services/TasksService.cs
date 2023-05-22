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

        public async Task<KanbanTask> CompleteTask(KanbanTask kanbanTask, Member member)
        {
            var path = $"{_apiPath}/{kanbanTask.Id}/complete/{member.Id}";
            var result = await GetData<KanbanTask>(path);
            kanbanTask.TaskCompleted = true;
            kanbanTask.CompletedById = member.Id;
            kanbanTask.CompletedBy = member;
            return result;
        }

        public async Task<KanbanTask> AssignTask(KanbanTask kanbanTask, Member member)
        {
            var path = $"{_apiPath}/{kanbanTask.Id}/assign/{member.Id}";
            var result = await GetData<KanbanTask>(path);
            kanbanTask.AssignedId = member.Id;
            kanbanTask.Assigned = member;
            return result;
        }

        public async Task DeleteTask(KanbanTask kanbanTask)
        {
            var path = $"{_apiPath}/{kanbanTask.Id}";
            await DeleteData(path);
        }
    }
}
