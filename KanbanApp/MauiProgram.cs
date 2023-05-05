namespace KanbanApp;

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

        // Viewmodel DI
        builder.Services.AddTransient<MainViewModel>();

		return builder.Build();
	}
}
