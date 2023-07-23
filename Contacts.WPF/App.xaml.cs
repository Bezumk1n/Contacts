using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Contacts.Persistance;
using Contacts.Application;
using Contacts.Services;
using Contacts.WPF.Views;
using Contacts.WPF.ViewModels;
using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contacts.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = CreateHostBuilder(new string[] { }).Build();
        }
        private IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);
            host.ConfigureServices((context, services) =>
            {
                services.AddViews();
                services.AddInfrastructureServices(context.Configuration);
                services.AddApplicationServices();
                services.AddServices();
            });
            return host;
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            var initialiser = _host.Services.GetRequiredService<ContactsDbContextInitialiser>();
            initialiser.Initialise();

            Window window = _host.Services.GetRequiredService<MainWindowV>();
            window.DataContext = _host.Services.GetRequiredService<MainWindowVM>();
            window.Show();

            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
