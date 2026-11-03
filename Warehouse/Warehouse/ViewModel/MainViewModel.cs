using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Warehouse.DTOs;
using Warehouse.Services;

namespace Warehouse.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private ObservableCollection<WarehouseListItem> _warehouses;
        public ObservableCollection<WarehouseListItem> Warehouses
        {
            get { return _warehouses; }
            set { _warehouses = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CabinetList> _cabinets;
        public ObservableCollection<CabinetList> Cabinets
        {
            get { return _cabinets; }
            set { _cabinets = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ShelfList> _shelves;
        public ObservableCollection<ShelfList> Shelves
        {
            get { return _shelves; }
            set { _shelves = value; OnPropertyChanged(); }
        }


        public MainViewModel( IServiceScopeFactory serviceScopeFactorys)
        {
            _serviceScopeFactory = serviceScopeFactorys;
        }   

        public async Task LoadWarehousesAsync()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var warehouseService = scope.ServiceProvider.GetRequiredService<WarehouseService>();
            Warehouses = new ObservableCollection<WarehouseListItem>(await warehouseService.GetWarehouses());
        }

        public async Task GetCabinetsByWarehouseIdAsync(int warehouseId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var warehouseService = scope.ServiceProvider.GetRequiredService<WarehouseService>();
            Cabinets = new ObservableCollection<CabinetList>(await warehouseService.GetCabinets(warehouseId));
        }

        public async Task GetShelvesByCabinetIdAsync(int cabinetId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var warehouseService = scope.ServiceProvider.GetRequiredService<WarehouseService>();
            Shelves = new ObservableCollection<ShelfList>(await warehouseService.GetShelves(cabinetId));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
