using KanbanApp.ViewModels;

namespace KanbanApp.Pages;

public partial class TaskPage : ContentPage
{
    private TaskViewModel _vm;
    public TaskPage(TaskViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}