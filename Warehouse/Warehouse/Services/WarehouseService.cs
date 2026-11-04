using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Warehouse.Database;
using Warehouse.DTOs;
using Warehouse.Entites;
using Warehouse.Entities;
using Warehouse.Logging;
using Warehouse.Model;

namespace Warehouse.Services
{
    public class WarehouseService
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly ILogger<WarehouseService> _logger;
        private readonly GuiLoggerProvider _loggerProvider;
        private double _minimumFreeSpaceLimit = 100;
        public WarehouseService(WarehouseDbContext dbContext, ILogger<WarehouseService> logger, GuiLoggerProvider loggerProvider)
        {
            _dbContext = dbContext;
            _logger = logger;
            _loggerProvider = loggerProvider;
        }


        public async Task<List<WarehouseListItem>> GetWarehouses()
        {
            return await _dbContext.Warehouses.Select(x =>
                new WarehouseListItem()
                {
                    Name = x.Name,
                    Address = x.Address
                }
            ).ToListAsync();
        }

        public async Task<List<CabinetList>> GetCabinets(int warehouseId)
        {
            var cabinets = await _dbContext.Cabinets
                 .Where(x => x.WarehouseId == warehouseId)
                 .ToListAsync();

            var cabinetList = cabinets.Select(x =>
                 new CabinetList()
                 {
                     Id = x.Id,
                     Name = x.Name
                 }).ToList();

            cabinetList.ForEach(
                x => x.HasFreeSpace = HasFreeSpaceOnCabinet(cabinets.First(y => y.Id == x.Id)));
            return cabinetList;
        }

        public async Task<List<ShelfList>> GetShelves(int cabinetId)
        {
            var cabinets = await _dbContext.Cabinets
                 .Where(x => x.Id == cabinetId)
                 .ToListAsync();

            var cabinetsShelves = cabinets
                .SelectMany(x => x.Shelves)
                .ToList();

            var cabinetList = cabinetsShelves.Select(x =>
                new ShelfList()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();



            cabinetList.ForEach(x => x.HasFreeSpace = HasFreeSpaceOnShelf(cabinetsShelves.First(y => y.Id == x.Id)));
            return cabinetList;
        }

        public async Task<CabinetDetails> GetCabinetDetails(int cabinetId)
        {
            var cabinet = await _dbContext.Cabinets.FirstAsync(x => x.Id == cabinetId);
            var shelves = cabinet.Shelves;


            var shelvesDetails = new List<ShelfDetails>();

            foreach (var shelf in shelves)
            {
                var productsDetails = new List<StockDetails>();

                foreach (var stock in shelf.Stocks)
                {
                    productsDetails.Add(new StockDetails()
                    {
                        ProductId = stock.ProductId,
                        PositionX = stock.PositionX,
                        PositionY = stock.PositionY,
                        PositionZ = stock.PositionZ,
                        ProductName = stock.Product.Name,
                        Quantity = stock.Quantity,
                        SKU = stock.Product.SKU
                    });
                }

                shelvesDetails.Add(new ShelfDetails()
                {
                    Id = shelf.Id,
                    Name = shelf.Name,
                    Width = shelf.Width,
                    Height = shelf.Height,
                    Depth = shelf.Depth,
                    Stocks = productsDetails
                });
            }

            return new CabinetDetails()
            {
                Id = cabinet.Id,
                Name = cabinet.Name,
                Shelves = shelvesDetails
            };
        }

        public async Task CreateCabinet(CabinetCreateModel cabinetCreateModel)
        {
            var newCabinet = new Cabinet()
            {
                WarehouseId = cabinetCreateModel.WarehouseId,
                Name = cabinetCreateModel.Name,
                
            };

            var shelves = cabinetCreateModel.Shelves.Select(x =>
                new Shelf()
                {
                    Name = x.Name,
                    Depth = x.Depth,
                    Height = x.Height,
                    Width = x.Width
                }).ToList();

            newCabinet.Shelves = shelves;

            _dbContext.Cabinets.Add(newCabinet);
            await _dbContext.SaveChangesAsync();
        }

        public bool HasFreeSpaceOnCabinet(Cabinet cabinet)
        {
            var hasFreeSpace = false;

            var shelves = cabinet.Shelves.ToList();
            if (shelves.Count == 0) return false;


            hasFreeSpace = shelves.Select(x => HasFreeSpaceOnShelf(x)).Any();
            return hasFreeSpace;
        }

        public bool HasFreeSpaceOnShelf(Shelf shelf)
        {
            var hasFreeSpace = false;
            var shelfsWidth = shelf.Width;
            var stocks = shelf.Stocks.OrderBy(x => x.PositionX).ToList();

            if (stocks.Count == 0 && shelfsWidth > _minimumFreeSpaceLimit)
            {
                hasFreeSpace = true;
                return hasFreeSpace;
            }


            for (int i = 0; i < stocks.Count; i++)
            {
                var isLastItem = i == stocks.Count - 1;
                var isFirstItem = i == 0;
                var productPositionX = stocks[i].PositionX;
                var productWidth = stocks[i].Product.Width;
                var productBoundary = productPositionX + productWidth;

                //If first item
                if (isFirstItem && (productPositionX - 0 > _minimumFreeSpaceLimit))
                {
                    hasFreeSpace = true;
                    break;
                }

                //If last item
                if (isLastItem && (shelfsWidth - productBoundary > _minimumFreeSpaceLimit))
                {
                    hasFreeSpace = true;
                    break;
                }

                //Between items
                if (!isLastItem)
                {
                    var nextProductPositionX = stocks[i + 1].PositionX;
                    if (nextProductPositionX - productBoundary > _minimumFreeSpaceLimit)
                    {
                        hasFreeSpace = true;
                        break;
                    }
                }
            }
            return hasFreeSpace;
        }
    }
}
