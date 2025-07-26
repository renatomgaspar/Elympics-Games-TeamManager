using Elympics_Games.Mobile.DTOs.User;
using Elympics_Games.Mobile.Models;
using Elympics_Games.Mobile.Services;
using Elympics_Games.Mobile.ViewModels;
using Microsoft.Extensions.Logging;

namespace Elympics_Games.Mobile
{
    public static class MauiProgram
    {
        public static IServiceProvider ServiceProvider { get; private set; }

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

            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<PasswordService<AuthUserDto>>();
            builder.Services.AddSingleton<PasswordService<CreateUserDto>>();

            builder.Services.AddTransient<SignupViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            ServiceProvider = app.Services;

            return app;
        }
    }
}
