using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Pages;
using KanbanApp.Services;

namespace KanbanApp.ViewModels
{

    public partial class CreateBoardViewModel : ObservableObject
    {
        private BoardsService _boardsService;

        [ObservableProperty] private Board _newBoard;

        public CreateBoardViewModel(BoardsService boardsService)
        {
            _boardsService = boardsService;
            NewBoard = new Board();
            NewBoard.OwnerId = 1;
        }
        [RelayCommand]
        async Task CreateBoard()
        {
            try
            {
                var newBoard = await _boardsService.PostBoard(NewBoard);
                var param = new Dictionary<string, object> { { "board", newBoard } };
                await Shell.Current.GoToAsync(nameof(BoardPage), param);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Fejl!", e.Message, "Ok");
            }
        }
    }
}
