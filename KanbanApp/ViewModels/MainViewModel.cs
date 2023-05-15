using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Pages;
using KanbanApp.Services;
using System.Collections.ObjectModel;

namespace KanbanApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private BoardsService _boardsService = new BoardsService();

        [ObservableProperty] private ObservableCollection<Board> testBoard;

        public MainViewModel()
        {
            TestBoard = new ObservableCollection<Board>();
            GetBoard();
        }

        private async void GetBoard()
        {
            //var result = await _boardsService.GetBoards();
            //foreach (var board in result)
            //{
            //    TestBoard.Add(board);
            //}
        }
        [RelayCommand]
        public async Task GoToBoard(Board board)
        {
            var param = new Dictionary<string, object> { { "board", board } };
            await Shell.Current.GoToAsync(nameof(BoardPage), param);
        }
    }
}
