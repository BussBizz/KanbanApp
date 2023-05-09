using KanbanApp.ViewModels;

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
}