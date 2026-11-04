using Microsoft.Extensions.Logging;
using System.Windows;
using Warehouse.Logging;
using Warehouse.ViewModel;

namespace Warehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger<MainWindow> _logger;
        private readonly GuiLoggerProvider _loggerProvider;
        private readonly MainViewModel _viewModel;
        public MainWindow(ILogger<MainWindow> logger, GuiLoggerProvider loggerProvider, MainViewModel viewModel)
        {
            InitializeComponent();
            _logger = logger;
            _loggerProvider = loggerProvider;
            _viewModel = viewModel;
            DataContext = _viewModel;

            _viewModel.CreateCabinet();
            _logger.LogInformation("MainWindow initialized.");
        }
    }
}