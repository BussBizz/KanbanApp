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
        builder.Services.AddTransient<SignupPage>();

        // Viewmodel DI
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<BoardViewModel>();
        builder.Services.AddTransient<SignupViewModel>();

        // Service DI
        builder.Services.AddTransient<BoardsService>();
        builder.Services.AddTransient<LoginService>();
        builder.Services.AddTransient<UserService>();

        return builder.Build();
    }
}
