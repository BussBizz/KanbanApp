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
        private MemberService _memberService;
        private InviteService _inviteService;

        [ObservableProperty] private ObservableCollection<Member> _membershipList;

        [ObservableProperty] private ObservableCollection<Invite> _inviteList;
        [ObservableProperty] private string _inviteButton;

        public MainViewModel(MemberService memberService, InviteService inviteService)
        {
            _memberService = memberService;
            _inviteService = inviteService;
            MembershipList = new ObservableCollection<Member>();
            InviteList = new ObservableCollection<Invite>();
            GetMemberships();
            GetInvites();
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

        private async void GetInvites()
        {
            var userId = await SecureStorage.GetAsync("userId");
            var result = await _inviteService.GetInvitesByUser(int.Parse(userId));
            foreach (var invite in result)
            {
                InviteList.Add(invite);
            }
            InviteButton = $"Invitationer ({InviteList.Count})";
        }

        [RelayCommand]
        public async Task GoToInvites()
        {
            var param = new Dictionary<string, object> { { "invites", InviteList }, { "memberships", MembershipList } };
            await Shell.Current.GoToAsync(nameof(UserInvitesPage), param);
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
    }
}
