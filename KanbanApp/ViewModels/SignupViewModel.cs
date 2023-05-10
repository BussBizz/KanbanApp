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
        private User _newUser;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private bool _nameTakenStatus;

        [ObservableProperty]
        private string _nameStatusText;

        public SignupViewModel()
        {
            _userService = new UserService();
            NewUser = new User();
            NewUser.IsAnon = false;
        }

        [RelayCommand]
        async Task SignupUser()
        {
            if (string.IsNullOrEmpty(NewUser.Name) || string.IsNullOrEmpty(NewUser.Password) || NameTakenStatus)
            {
                await Shell.Current.DisplayAlert("Fejl i oprettelse!", "Prøv igen.", "Ok ;(");
                return;
            }

            try
            {
                await _userService.CreateUser(NewUser);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Fejl!", e.Message, "Ok");
            }
        }

        async partial void OnUserNameChanged(string value)
        {
            if (NewUser.Name == UserName || UserName == String.Empty) return;

            NewUser.Name = UserName;
            NameTakenStatus = await _userService.CheckUserName(NewUser.Name);
        }

        partial void OnNameTakenStatusChanged(bool value)
        {
            NameStatusText = NameTakenStatus ? "Brugernavn er taget! Vælg et nyt, taber. :´-(" : string.Empty;
        }
    }
}
