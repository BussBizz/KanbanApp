using KanbanApp.Services;

namespace KanbanApp;

public partial class SettingsPage : ContentPage
{
    private readonly AuthService _authService;
    public SettingsPage(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    private async void OnLogoutClick(object sender, EventArgs e)
    {
        if (await DisplayAlert("Are you sure?", "You will be logged out.", "Yes", "No"))
        {
            await _authService.DeleteToken();
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}