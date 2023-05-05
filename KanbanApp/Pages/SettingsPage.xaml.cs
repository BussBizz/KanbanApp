namespace KanbanApp;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private async void OnLogoutClick(object sender, EventArgs e)
    {
        if (await DisplayAlert("Are you sure?", "You will be logged out.", "Yes", "No"))
        {
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
    
}