using Microsoft.Extensions.Logging;
using Contacts.Persistance;
using Contacts.Application;
using Contacts.Services;
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
                    fonts.AddFont("fewriter_memesbruh03.ttf", "Fewriter");
                });
                
            builder.Configuration.AddConfiguration(jsonConfuguration);
            builder.Services.AddPersistanceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddServices();
            builder.Services.AddViews();

            var initialiser = builder.Services.BuildServiceProvider().GetService<ContactsDbContextInitialiser>();
            initialiser.Initialise();

            return builder.Build();
        }
    }
}