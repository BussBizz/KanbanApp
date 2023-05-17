using KanbanApp.ViewModels;

namespace KanbanApp;

public partial class LoginPage : ContentPage
{
    private LoginViewModel _vm;
	public LoginPage(LoginViewModel vm)
	{
        _vm = vm;
        BindingContext = _vm;
		InitializeComponent();
	}
    
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