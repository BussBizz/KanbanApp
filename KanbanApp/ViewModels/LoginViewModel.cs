using CommunityToolkit.Mvvm.ComponentModel;
using KanbanApp.Models;
using KanbanApp.Services;

namespace KanbanApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly UserService _userService;

        [ObservableProperty]
        private User _currentUser;

        public LoginViewModel()
        {
            _userService = new UserService();
        }


    }
}
