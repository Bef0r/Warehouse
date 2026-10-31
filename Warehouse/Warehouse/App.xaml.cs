using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Warehouse.Database;

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

            services.AddDbContext<WarehouseDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("WarehouseDb")));

            _serviceProvider = services.BuildServiceProvider();

            _serviceProvider = services.BuildServiceProvider();

            using var scope = _serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<WarehouseDbContext>();

            var canConnect = dbContext.Database.CanConnect();
        }
    }

}
