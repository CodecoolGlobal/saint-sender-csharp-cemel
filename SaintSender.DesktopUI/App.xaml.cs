using Microsoft.Extensions.DependencyInjection;
using SaintSender.DesktopUI.DatabaseRelated;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
        }

        public void ConfigureServices(IServiceCollection services)
        => services.AddDbContext<AppDbContext>();
    }
}
