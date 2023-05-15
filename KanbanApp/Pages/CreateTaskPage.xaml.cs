using KanbanApp.ViewModels;

namespace KanbanApp.Pages;

public partial class CreateTaskPage : ContentPage
{
    private readonly CreateTaskViewModel _vm;
    public CreateTaskPage(CreateTaskViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}