using KanbanApp.ViewModels;
using Microsoft.Maui.Controls;

namespace KanbanApp.Pages;

public partial class BoardPage : ContentPage
{
    private BoardViewModel _vm;

    public BoardPage(BoardViewModel vm)
    {
        _vm = vm;
        BindingContext = _vm;
        InitializeComponent();
    }
}