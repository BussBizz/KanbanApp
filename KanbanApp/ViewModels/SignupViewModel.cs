using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Services;

namespace KanbanApp.ViewModels
{
    public partial class SignupViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly LoginService _loginService;

        [ObservableProperty]
        private Password _newLogin;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private bool _nameTakenStatus;

        [ObservableProperty]
        private string _nameStatusText;

        public SignupViewModel()
        {
            _userService = new UserService();
            _loginService = new LoginService();
            NewLogin = new Password();
        }

        [RelayCommand]
        async Task SignupUser()
        {
            if (string.IsNullOrEmpty(NewLogin.User.Name) || string.IsNullOrEmpty(NewLogin.Hash) || NameTakenStatus)
            {
                await Shell.Current.DisplayAlert("Fejl i oprettelse!", "Prøv igen.", "Ok ;(");
                return;
            }

            try
            {
                await _userService.CreateUser(NewLogin);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Fejl!", e.Message, "Ok");
            }
        }

        async partial void OnUserNameChanged(string value)
        {
            if (NewLogin.User.Name == UserName || UserName == String.Empty) return;

            NewLogin.User.Name = UserName;
            NameTakenStatus = await _loginService.CheckUserName(NewLogin.User.Name);
        }

        partial void OnNameTakenStatusChanged(bool value)
        {
            NameStatusText = NameTakenStatus ? "Brugernavn er taget! Vælg et nyt, taber. :´-(" : string.Empty;
        }
    }
}
