using Contacts.WPF.Common;
using Contacts.WPF.Common.Navigation;
using Contacts.WPF.ViewModels;
using Contacts.WPF.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.WPF
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            // Фабрика ViewModel
            services.AddSingleton<Func<Type, IViewModel>>(serviceProvider => viewModelType => (IViewModel)serviceProvider.GetRequiredService(viewModelType));

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<MainWindowV>();
            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<ContactsVM>();
            services.AddTransient<AddContactVM>();
            services.AddTransient<UpdateContactVM>();
            services.AddTransient<InfoVM>();
            return services;
        }
    }
}
