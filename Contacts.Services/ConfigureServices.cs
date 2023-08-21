using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Common;
using Contacts.Domain.Entities;
using Contacts.Services.HttpClients;
using Contacts.Services.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IStore<Contact>, ContactsStore>();
            services.AddSingleton<IStore<User>, UserStore>();
            services.AddHttpClient<ICommonApiClient,CommonApiClent>();
            return services;
        }
    }
}
