using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Windows;
using Warehouse.Database;
using Warehouse.Logging;
using Warehouse.Services;
using Warehouse.ViewModel;

namespace Warehouse
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<MainWindow>();

            services.AddSingleton<GuiLoggerProvider>();
            services.AddSingleton<ILoggerProvider>(sp => sp.GetRequiredService<GuiLoggerProvider>());


            services.AddTransient<MainViewModel>();

            services.AddScoped<WarehouseService>();

            services.AddLogging();


            services.AddDbContext<WarehouseDbContext>(options =>
                options.
                UseLazyLoadingProxies().
                UseNpgsql(configuration.GetConnectionString("WarehouseDb")));

            _serviceProvider = services.BuildServiceProvider();

            using var scope = _serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<WarehouseDbContext>();

            var canConnect = dbContext.Database.CanConnect();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
