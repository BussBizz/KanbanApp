using CommunityToolkit.Mvvm.ComponentModel;
using KanbanApp.Models;
using KanbanApp.Services;

namespace KanbanApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private BoardsService _boardsService = new BoardsService();

        [ObservableProperty] public Board testBoard;

        public MainViewModel()
        {
            GetBoard();
        }

        private async void GetBoard()
        {
            var result = await _boardsService.GetBoard(1);
            TestBoard = result;
        }
    }
}
