using Microsoft.Extensions.Logging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warehouse.Logging;

namespace Warehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger<MainWindow> _logger;
        private readonly GuiLoggerProvider _loggerProvider;
        public MainWindow(ILogger<MainWindow> logger, GuiLoggerProvider loggerProvider)
        {
            InitializeComponent();
            _logger = logger;
            _loggerProvider = loggerProvider;

            _logger.LogInformation("MainWindow initialized.");
            var numberOfLogs = _loggerProvider.Logs.Count;
            _loggerProvider.ClearLogs();
        }
    }
}