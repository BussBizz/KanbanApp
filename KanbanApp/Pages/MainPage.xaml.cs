using KanbanApp.ViewModels;
using System.Runtime.CompilerServices;

namespace KanbanApp;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _vm;
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    private async void OnNavigateToSettings(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }
    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
}

