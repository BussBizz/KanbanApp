using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KanbanApp.Models;
using KanbanApp.Services;

namespace KanbanApp.ViewModels
{
    public partial class SignupViewModel : ObservableObject
    {
        private readonly UserService _userService;

        [ObservableProperty]
        private Password _newLogin;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private bool _nameTakenStatus;

        [ObservableProperty]
        private string _nameStatusText;

        public SignupViewModel(UserService userService)
        {
            _userService = userService;
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
                NewLogin.User.IsAnon = false;
                var user = await _userService.CreateUser(NewLogin);

                var param = new Dictionary<string, object> { { "username", user.Name } };
                await Shell.Current.GoToAsync("..", param);
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
            NameTakenStatus = await _userService.CheckUserName(NewLogin.User.Name);
        }

        partial void OnNameTakenStatusChanged(bool value)
        {
            NameStatusText = NameTakenStatus ? "Brugernavn er taget! Vælg et nyt, taber. :´-(" : string.Empty;
        }
    }
}
