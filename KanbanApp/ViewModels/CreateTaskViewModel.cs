using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Pages;
using KanbanApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.ViewModels
{
    public partial class CreateTaskViewModel : ObservableObject
    {
        private TasksService _tasksService = new TasksService();

        [ObservableProperty] private KanbanTask _newKanbanTask;

        public CreateTaskViewModel()
        {
            NewKanbanTask = new KanbanTask();
        }
        [RelayCommand]
        async Task CreateBoard()
        {
            try
            {
                var newKanbanTask = await _tasksService.PostTask(NewKanbanTask);
                var param = new Dictionary<string, object> { { "task", newKanbanTask } };
                await Shell.Current.GoToAsync(nameof(BoardPage), param);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Fejl!", e.Message, "Ok");
            }
        }
    }
}
