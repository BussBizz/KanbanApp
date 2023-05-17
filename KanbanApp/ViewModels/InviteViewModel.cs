using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Services;
using System.Collections.ObjectModel;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(CurrentBoard), "board")]
    public partial class InviteViewModel : ObservableObject
    {
        private readonly InviteService _inviteService;
        private readonly UserService _userService;

        [ObservableProperty] private Board _currentBoard;
        [ObservableProperty] private ObservableCollection<Invite> _invites;
        [ObservableProperty] private string _username;
        [ObservableProperty] private string _usernameStatus;
        [ObservableProperty] private string _inviteButton;
        [ObservableProperty] private bool _inviteButtonEnabled = true;

        private bool _anon = true;

        public InviteViewModel(InviteService inviteService, UserService userService)
        {
            _inviteService = inviteService;
            _userService = userService;
            InviteButton = "Inviter ven";
            Invites = new ObservableCollection<Invite>();
        }

        [RelayCommand]
        public async Task CreateInvite()
        {
            var newInvite = new Invite();
            if (_anon)
            {
                newInvite = await _inviteService.CreateAnonInvite(CurrentBoard);
                await Shell.Current.DisplayAlert($"{newInvite.Code}", $"Send koden til ny {CurrentBoard.Titel} deltager.", "Ok");
            }
            else
            {
                var user = await _userService.GetUserIdFromName(Username);
                newInvite = await _inviteService.CreateUserInvite(CurrentBoard, user);
                await Shell.Current.DisplayAlert("Invitaion sendt.", $"{Username} er nu inviteret til {CurrentBoard.Titel}.", "Ok");
            }
            Invites.Add(newInvite);
            Username = "";
        }

        [RelayCommand]
        public async Task DeleteInvite(Invite invite)
        {
            Invites.Remove(invite);
            await _inviteService.DeleteInvite(invite);
        }

        async partial void OnCurrentBoardChanged(Board value)
        {
            var invites = await _inviteService.GetInvitesByBoard(value.Id);
            foreach (var invite in invites)
                Invites.Add(invite);
        }

        async partial void OnUsernameChanged(string value)
        {
            _anon = string.IsNullOrEmpty(value) ? true : !await _userService.CheckUserName(value);
            if (_anon)
            {
                UsernameStatus = "Brugeren findes ikke, inviter anonym bruger";
                InviteButton = "Inviter ven";
                InviteButtonEnabled = true;
            }
            else
            {
                if (CurrentBoard.Members.Any(m => m.User.Name == Username))
                {
                    UsernameStatus = $"{value} er allerede medlem";
                    InviteButtonEnabled = false;
                }
                else if (Invites.Any(i => i.User?.Name == Username))
                {
                    UsernameStatus = $"{value} er allerede inviteret";
                    InviteButtonEnabled = false;
                }
                else
                {
                    UsernameStatus = string.Empty;
                    InviteButtonEnabled = true;
                }
                InviteButton = $"Inviter {value}";
            }
        }
    }
}
