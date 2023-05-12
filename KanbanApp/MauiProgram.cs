namespace KanbanApp;

using KanbanApp.Pages;
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

        // Viewmodel DI
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<BoardViewModel>();
        builder.Services.AddTransient<CreateBoardViewModel>();
        builder.Services.AddTransient<CreateCategoryViewModel>();
        builder.Services.AddTransient<CreateTaskViewModel>();

        return builder.Build();
    }
}
