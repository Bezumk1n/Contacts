using Microsoft.Extensions.Logging;
using Contacts.Persistance;
using Contacts.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Reflection;

namespace Contacts.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var path = Path.GetDirectoryName(assembly.Location) + "\\appsettings.json";

            var jsonConfuguration = new ConfigurationBuilder()
                        .AddJsonFile(path)
                        .Build();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
                
            builder.Configuration.AddConfiguration(jsonConfuguration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            var initialiser = builder.Services.BuildServiceProvider().GetService<ContactsDbContextInitialiser>();
            initialiser.Initialise();

            return builder.Build();
        }
    }
}