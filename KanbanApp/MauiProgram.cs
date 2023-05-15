namespace KanbanApp;

using KanbanApp.Pages;
using KanbanApp.Services;
using KanbanApp.ViewModels;
using Syncfusion.Maui.Core.Hosting;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
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

        // Viewmodel DI
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<BoardViewModel>();
        builder.Services.AddTransient<CreateBoardViewModel>();
        builder.Services.AddTransient<CreateCategoryViewModel>();
        builder.Services.AddTransient<CreateTaskViewModel>();
        builder.Services.AddTransient<SignupViewModel>();
        builder.Services.AddTransient<InviteViewModel>();

        // Service DI
        builder.Services.AddTransient<BoardsService>();
        builder.Services.AddTransient<LoginService>();
        builder.Services.AddTransient<UserService>();
        builder.Services.AddTransient<CategoryService>();
        builder.Services.AddTransient<MemberService>();
        builder.Services.AddTransient<InviteService>();

        return builder.Build();
    }
}
