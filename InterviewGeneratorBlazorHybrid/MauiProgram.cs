using CommunityToolkit.Maui;
using InterviewGeneratorBlazorHybrid.Data;
using InterviewGeneratorBlazorHybrid.ViewModels;
using Microsoft.Extensions.Logging;

namespace InterviewGeneratorBlazorHybrid
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
                });

            // RegisterViewModels
            //var connectionString = "Data Source=c:\\temp\\app.db";
            builder.Services.AddSingleton(new AppDbContextFactory());

            builder.Services.AddScoped<CategoryViewModel>();
            builder.Services.AddScoped<QuestionViewModel>();
            builder.Services.AddScoped<InterviewViewModel>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
