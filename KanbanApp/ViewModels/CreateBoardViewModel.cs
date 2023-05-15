using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Pages;
using KanbanApp.Services;
using System.Collections.ObjectModel;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(Memberships), "memberships")]
    public partial class CreateBoardViewModel : ObservableObject
    {
        private readonly BoardsService _boardsService;
        private readonly MemberService _memberService;

        [ObservableProperty] private ObservableCollection<Member> _memberships;
        [ObservableProperty] private Board _newBoard;

        public CreateBoardViewModel(BoardsService boardsService, MemberService memberService)
        {
            _boardsService = boardsService;
            _memberService = memberService;
            NewBoard = new Board();
        }

        [RelayCommand]
        async Task CreateBoard()
        {
            try
            {
                var userId = int.Parse(await SecureStorage.GetAsync("userId"));

                NewBoard.OwnerId = userId;
                var newBoard = await _boardsService.PostBoard(NewBoard);

                var newMembership = new Member
                {
                    UserId = userId,
                    BoardId = newBoard.Id,
                    CanCreate = true,
                    CanAssign = true,
                };

                var membership = await _memberService.CreateMembership(newMembership);
                membership.Board = newBoard;
                Memberships.Add(membership);

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
