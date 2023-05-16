using KanbanApp.ViewModels;

namespace KanbanApp.Pages;

public partial class UserInvitesPage : ContentPage
{
	private UserInvitesViewModel _vm;
	public UserInvitesPage(UserInvitesViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}