using KanbanApp.Pages;

namespace KanbanApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
        Routing.RegisterRoute(nameof(BoardPage), typeof(BoardPage));
        Routing.RegisterRoute(nameof(CreateBoardPage), typeof(CreateBoardPage));
        Routing.RegisterRoute(nameof(CreateCategoryPage), typeof(CreateCategoryPage));
        Routing.RegisterRoute(nameof(CreateTaskPage), typeof(CreateTaskPage));
        Routing.RegisterRoute(nameof(InvitePage), typeof(InvitePage));
        Routing.RegisterRoute(nameof(UserInvitesPage), typeof(UserInvitesPage));
        Routing.RegisterRoute(nameof(TaskPage), typeof(TaskPage));
        Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
    }
}
