using KanbanApp.ViewModels;

namespace KanbanApp.Pages;

public partial class InvitePage : ContentPage
{
	private readonly InviteViewModel _vm;
	public InvitePage(InviteViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}