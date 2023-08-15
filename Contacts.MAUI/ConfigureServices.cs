using Contacts.MAUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.MAUI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddSingleton<ContactsVM>();
            services.AddSingleton<ContactsV>(service => new ContactsV(service.GetRequiredService<ContactsVM>()));
            return services;
        }
    }
}
