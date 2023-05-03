namespace KanbanApp;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
	private async void OnLoginClicked(object sender, EventArgs e)
	{
		if (usernameEntry.Text == null || passwordEntry.Text == null)
		{
			validationLabel.Text = "Der er felter som ikke er udfyldt korrekt"; return;
		}
        validationLabel.Text = string.Empty;
        await Navigation.PopAsync();
    }
}