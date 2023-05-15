using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Pages;
using KanbanApp.Services;
using System.Collections.ObjectModel;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(NewBoard), "newBoard")]
    public partial class MainViewModel : ObservableObject
    {
        private BoardsService _boardsService;

        [ObservableProperty] private ObservableCollection<Board> _boardList;
        [ObservableProperty] private Board? _newBoard;
        public MainViewModel(BoardsService boardsService)
        {
            _boardsService = boardsService;
            BoardList = new ObservableCollection<Board>();
            GetBoard();
        }

        private async void GetBoard()
        {
            var result = await _boardsService.GetBoards();
            foreach (var board in result)
            {
                BoardList.Add(board);
            }
        }
        [RelayCommand]
        public async Task GoToBoard(Board board)
        {
            var param = new Dictionary<string, object> { { "board", board } };
            await Shell.Current.GoToAsync(nameof(BoardPage), param);
        }
        [RelayCommand]
        public async Task GoToCreateBoard()
        {
            await Shell.Current.GoToAsync(nameof(CreateBoardPage));
        }

        partial void OnNewBoardChanged(Board value)
        {
            if (value != null && !BoardList.Any(board => board.Id == value.Id))
            {
                BoardList.Add(value);
            }
            NewBoard = null;
        }
    }
}
