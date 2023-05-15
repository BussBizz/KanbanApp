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
        private readonly LoginService _loginService;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        public LoginViewModel(LoginService loginService)
        {
            _loginService = loginService;
            LoginAttempt();
        }

        [RelayCommand]
        public async Task Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await Shell.Current.DisplayAlert("Manglende info", "Indtast brugernavn og kodeord.", "Ok");
                return;
            }

            var auth = Encoding.UTF8.GetBytes($"{Username}:{Password}");

            var test = Convert.ToBase64String(auth);

            await SecureStorage.SetAsync("creds", test);
            await SecureStorage.SetAsync("username", Username);

            await LoginAttempt();
        }

        private async Task LoginAttempt()
        {
            var username = await SecureStorage.GetAsync("username") ?? Username;
            if (string.IsNullOrEmpty(username)) return;

            User? user = null;
            try
            {
                user = await _loginService.Login(username);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Fejl", e.Message, "Ok");
            }

            if (user == null)
            {
                SecureStorage.RemoveAll();
                return;
            }

            await SecureStorage.SetAsync("userId", user.Id.ToString());
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}
