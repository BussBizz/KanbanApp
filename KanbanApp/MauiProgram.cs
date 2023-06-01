namespace KanbanApp;
using KanbanApp.Models;
using KanbanApp.Pages;
using KanbanApp.Services;
using KanbanApp.ViewModels;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Pages DI
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<BoardPage>();
        builder.Services.AddTransient<CreateBoardPage>();
        builder.Services.AddTransient<CreateCategoryPage>();
        builder.Services.AddTransient<CreateTaskPage>();
        builder.Services.AddTransient<SignupPage>();
        builder.Services.AddTransient<InvitePage>();
        builder.Services.AddTransient<UserInvitesPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<TaskPage>();
        builder.Services.AddTransient<AdminPage>();

        // Viewmodel DI
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<BoardViewModel>();
        builder.Services.AddTransient<CreateBoardViewModel>();
        builder.Services.AddTransient<CreateCategoryViewModel>();
        builder.Services.AddTransient<CreateTaskViewModel>();
        builder.Services.AddTransient<SignupViewModel>();
        builder.Services.AddTransient<InviteViewModel>();
        builder.Services.AddTransient<UserInvitesViewModel>();
        builder.Services.AddTransient<TaskViewModel>();
        builder.Services.AddTransient<AdminViewModel>();

        // Service DI
        builder.Services.AddTransient<BoardsService>();
        builder.Services.AddTransient<AuthService>();
        builder.Services.AddTransient<UserService>();
        builder.Services.AddTransient<CategoryService>();
        builder.Services.AddTransient<MemberService>();
        builder.Services.AddTransient<InviteService>();
        builder.Services.AddTransient<CommentsService>();
        builder.Services.AddTransient<TasksService>();

        return builder.Build();
    }
}
