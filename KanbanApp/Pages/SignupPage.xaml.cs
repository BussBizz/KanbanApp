using KanbanApp.ViewModels;

namespace KanbanApp;

public partial class SignupPage : ContentPage
{
    private SignupViewModel _vm;
	public SignupPage(SignupViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
	}

    //private async void OnCreateClicked(object sender, EventArgs e)
    //{
    //    if (usernameEntry.Text == null || passwordEntry.Text == null)
    //    {
    //        await DisplayAlert("Ugyldigt", "Der er felter som ikke er udfyldt korrekt", "OK");
    //        return;
    //    }
    //    await DisplayAlert("Oprettet", "Brugeren er oprettet", "OK");
    //}
}