using KanbanApp.ViewModels;

namespace KanbanApp.Pages;

public partial class AdminPage : ContentPage
{
	private readonly AdminViewModel _vm;
	public AdminPage(AdminViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}