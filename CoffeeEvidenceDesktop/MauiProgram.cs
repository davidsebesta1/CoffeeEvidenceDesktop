using CoffeeEvidenceDesktop.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using System.Text;

namespace CoffeeEvidenceDesktop
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<CoffeeFormPage>();
            builder.Services.AddSingleton<CoffeeUsagePage>();

            HttpClient client = new HttpClient();
            builder.Services.AddSingleton(client);

            builder.Services.AddSingleton<CoffeeUsageViewModel>();
            builder.Services.AddSingleton<CoffeeFormViewModel>();

            return builder.Build();
        }
    }
}