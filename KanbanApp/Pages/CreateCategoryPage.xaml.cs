using KanbanApp.ViewModels;

namespace KanbanApp.Pages;

public partial class CreateCategoryPage : ContentPage
{
    private readonly CreateCategoryViewModel _vm;
    public CreateCategoryPage(CreateCategoryViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}