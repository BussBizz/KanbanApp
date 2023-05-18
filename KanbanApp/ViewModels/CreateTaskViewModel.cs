using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Pages;
using KanbanApp.Services;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(Category), "category")]
    public partial class CreateTaskViewModel : ObservableObject
    {
        private TasksService _tasksService;

        [ObservableProperty] private Category _category;
        [ObservableProperty] private KanbanTask _newKanbanTask;

        public CreateTaskViewModel()
        {
            _tasksService = new TasksService();
            NewKanbanTask = new KanbanTask();
        }
        [RelayCommand]
        async Task CreateTask()
        {
            try
            {
                var userId = int.Parse(await SecureStorage.GetAsync("userId"));
                NewKanbanTask.CreatorId = userId;
                NewKanbanTask.CategoryId = Category.Id;
                var newKanbanTask = await _tasksService.PostTask(NewKanbanTask);
                Category.KanbanTasks.Add(newKanbanTask);
                var param = new Dictionary<string, object> { { "task", newKanbanTask } };
                await Shell.Current.GoToAsync(nameof(BoardPage));
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Fejl!", e.Message, "Ok");
            }
        }
    }
}
