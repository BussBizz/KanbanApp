using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Pages;
using KanbanApp.Services;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(Category), "category")]
    [QueryProperty(nameof(CurrentMember), "currentMember")]
    public partial class CreateTaskViewModel : ObservableObject
    {
        private TasksService _tasksService;

        [ObservableProperty] private Category _category;
        [ObservableProperty] private Member _currentMember;
        [ObservableProperty] private KanbanTask _newKanbanTask;
        // Timepicker not bindable to DateTime.TimeOfDay
        [ObservableProperty] private TimeSpan _timeSpanFix;
        [ObservableProperty] bool _hasDeadline;

        public CreateTaskViewModel(TasksService tasksService)
        {
            _tasksService = tasksService;
            NewKanbanTask = new KanbanTask();
            HasDeadline = false;
        }

        [RelayCommand]
        async Task CreateTask()
        {
            try
            {
                NewKanbanTask.Deadline = NewKanbanTask.Deadline.Value.Date + TimeSpanFix;
                NewKanbanTask.CreatorId = CurrentMember.Id;
                NewKanbanTask.CategoryId = Category.Id;
                var newKanbanTask = await _tasksService.PostTask(NewKanbanTask);
                Category.KanbanTasks.Add(newKanbanTask);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Fejl!", e.Message, "Ok");
            }
        }

        partial void OnHasDeadlineChanged(bool value)
        {
            NewKanbanTask.Deadline = value ? DateTime.Now.AddDays(7) : null;
        }
    }
}
