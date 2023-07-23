using Contacts.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistance
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Настройка подключения к БД
            var connectionString = configuration["DbConnection"];

            //services.AddDbContext<ContactsDbContext>(options =>
            //    options.UseNpgsql(connectionString,
            //        builder => builder.MigrationsAssembly(typeof(ContactsDbContext).Assembly.FullName)));

            services.AddDbContext<ContactsDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddTransient<IContactsDbContext>(provider => provider.GetRequiredService<ContactsDbContext>());
            services.AddTransient<ContactsDbContextInitialiser>();

            return services;
        }
    }
}
