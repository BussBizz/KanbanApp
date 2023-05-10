using KanbanApp.ViewModels;

namespace KanbanApp;

public partial class LoginPage : ContentPage
{
    private LoginViewModel _vm;
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
	}
	//private async void OnLoginClicked(object sender, EventArgs e)
	//{
	//	if (usernameEntry.Text == null || passwordEntry.Text == null)
	//	{
 //          await DisplayAlert("Ugyldigt", "Der er felter som ikke er udfyldt korrekt", "OK");
 //          return;
 //       }
 //       await SecureStorage.SetAsync("hasAuth", "true");
 //       await Shell.Current.GoToAsync(nameof(MainPage));
 //   }
    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
    private async void OnNewUserClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignupPage));
    }
}