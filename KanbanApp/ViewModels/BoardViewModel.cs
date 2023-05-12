using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Pages;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(CurrentBoard), "board")]
    public partial class BoardViewModel : ObservableObject
    {
        [ObservableProperty] private Board _currentBoard;
        public async void BackButton()
        {
            foreach (var page in Shell.Current.Navigation.NavigationStack)
            {
                if (page != null && (page.GetType() == typeof(CreateBoardPage)))
                {
                    Shell.Current.Navigation.RemovePage(page);
                    break;
                }
            }
            var param = new Dictionary<string, object> { { "newBoard", CurrentBoard } };

            await Shell.Current.GoToAsync("..", param);
        }
        [RelayCommand]
        public async Task GoToCreatetask()
        {
            await Shell.Current.GoToAsync(nameof(CreateTaskPage));
        }
        [RelayCommand]
        public async Task GoToCreateCategory()
        {
            var param = new Dictionary<string, object> { { "board", CurrentBoard } };
            await Shell.Current.GoToAsync(nameof(CreateCategoryPage), param);
        }
    }
}
