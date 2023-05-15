using KanbanApp.ViewModels;
using Microsoft.Maui.Controls;

namespace KanbanApp.Pages;

public partial class BoardPage : ContentPage
{
    private BoardViewModel _vm;

    public BoardPage(BoardViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
    protected override bool OnBackButtonPressed()
    {
        _vm.BackButton();
        return true;
    }

    
}