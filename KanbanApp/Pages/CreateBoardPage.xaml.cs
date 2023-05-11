using KanbanApp.ViewModels;

namespace KanbanApp.Pages;

public partial class CreateBoardPage : ContentPage
{
	private CreateBoardViewModel _vm;
	public CreateBoardPage(CreateBoardViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}