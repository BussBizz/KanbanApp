using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Services;
using System.Runtime.CompilerServices;
using System.Text;

namespace KanbanApp.ViewModels
{
    [QueryProperty(nameof(Username), "username")]
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _loginService;
        private readonly InviteService _inviteService;
        private readonly MemberService _memberService;
        private readonly UserService _userService;

        [ObservableProperty] private string _username;
        [ObservableProperty] private string _password;
        [ObservableProperty] private string _code;

        public LoginViewModel(AuthService loginService, InviteService inviteService, MemberService memberService, UserService userService)
        {
            _loginService = loginService;
            _inviteService = inviteService;
            _memberService = memberService;
            _userService = userService;
            TokenLogin();
        }

        partial void OnCodeChanged(string value)
        {
            Code = value.ToUpper();
        }

        [RelayCommand]
        public async Task CodeLogin()
        {
            var invite = await _inviteService.GetInviteByCode(Code);
            if (invite == null)
            {
                await Shell.Current.DisplayAlert("Kunne ikke finde invitation", $"Ingen invitaion med kode: {Code}", "Ok");
                return;
            }

            var member = new Member { BoardId = invite.BoardId };
            var user = new User { Name = "Anon-" + Code, Memberships = { member } };
            var password = Guid.NewGuid().ToString();

            await _userService.CreateUser(new Models.Password { Hash = password, User = user });
            await _inviteService.DeleteInvite(invite);

            Username = user.Name;
            Password = password;

            await Login();
        }

        private async Task TokenLogin()
        {
            var token = await SecureStorage.GetAsync("token");

            if (!string.IsNullOrEmpty(token))
                if (await _loginService.Login(token))
                    await Shell.Current.GoToAsync(nameof(MainPage));
        }

        [RelayCommand]
        public async Task Login()
        {
            try
            {
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    await Shell.Current.DisplayAlert("Manglende info", "Indtast brugernavn og kodeord.", "Ok");
                    return;
                }

                var user = await _loginService.Login(Username, Password);
                if (user == null)
                {
                    await Shell.Current.DisplayAlert("Fejl", "Fokert brugernavn og kodeord.", "Ok");
                    return;
                }

                await SecureStorage.SetAsync("userId", user.Id.ToString());
                await SecureStorage.SetAsync("username", user.Name);

                await Shell.Current.GoToAsync(nameof(MainPage));
            }
            catch (HttpRequestException)
            {
                await Shell.Current.DisplayAlert("Der skete en fejl.", $"Ingen forbindelse til serveren, ring til Sigurd :)", "Ok");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Der skete en fejl.", $"Fejlbesked: {e.Message}", "Ok");
            }
        }
    }
}
