using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Services;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(CurrentBoard), "board")]
    public partial class InviteViewModel : ObservableObject
    {
        private readonly InviteService _inviteService;
        private readonly LoginService _loginService;
        private readonly UserService _userService;

        [ObservableProperty] private Board _currentBoard;
        [ObservableProperty] private Invite _newInvite;
        [ObservableProperty] private string _username;
        [ObservableProperty] private string _usernameStatus;
        [ObservableProperty] private string _inviteButton;

        private bool _anon = true;

        public InviteViewModel(InviteService inviteService, LoginService loginService, UserService userService)
        {
            _inviteService = inviteService;
            _loginService = loginService;
            _userService = userService;
            InviteButton = "Inviter ven";
        }

        [RelayCommand]
        public async Task CreateInvite()
        {
            if (_anon)
            {
                NewInvite = await _inviteService.CreateAnonInvite(CurrentBoard);
                await Shell.Current.DisplayAlert($"{NewInvite.Code}", $"Send koden til ny {CurrentBoard.Titel} deltager.", "Ok");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                var user = await _userService.GetUserIdFromName(Username);
                NewInvite = await _inviteService.CreateUserInvite(CurrentBoard, user);
                await Shell.Current.DisplayAlert("Invitaion sendt.", $"{Username} er nu inviteret til {CurrentBoard.Titel}.", "Ok");
                await Shell.Current.GoToAsync("..");
            }
        }

        async partial void OnUsernameChanged(string value)
        {
            _anon = string.IsNullOrEmpty(value) ? true : !await _loginService.CheckUserName(value);
            if (_anon)
            {
                UsernameStatus = "Brugeren findes ikke";
                InviteButton = "Inviter ven";
            }
            else
            {
                UsernameStatus = string.Empty;
                InviteButton = $"Inviter {value}";
            }
        }
    }
}
