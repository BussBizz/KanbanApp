using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Pages;
using KanbanApp.Services;
using System.Collections.ObjectModel;

namespace KanbanApp.ViewModels
{
    //[QueryProperty(nameof(NewBoard), "newBoard")]
    public partial class MainViewModel : ObservableObject
    {
        //private BoardsService _boardsService;
        private MemberService _memberService;

        [ObservableProperty] private ObservableCollection<Member> _membershipList;
        //[ObservableProperty] private Board? _newBoard;
        public MainViewModel(BoardsService boardsService, MemberService memberService)
        {
            //_boardsService = boardsService;
            _memberService = memberService;
            MembershipList = new ObservableCollection<Member>();
            GetMemberships();
        }

        private async void GetMemberships()
        {
            var userId = await SecureStorage.GetAsync("userId");
            var result = await _memberService.GetMembershipsByUser(int.Parse(userId));
            foreach (var membership in result)
            {
                MembershipList.Add(membership);
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
            var param = new Dictionary<string, object> { { "memberships", MembershipList } };
            await Shell.Current.GoToAsync(nameof(CreateBoardPage), param);
        }

        //partial void OnNewBoardChanged(Board value)
        //{
        //    if (value != null && !BoardList.Any(board => board.Id == value.Id))
        //    {
        //        BoardList.Add(value);
        //    }
        //    NewBoard = null;
        //}
    }
}
