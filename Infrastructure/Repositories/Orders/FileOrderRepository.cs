using iPlanner.Application.DTO.Orders;
using iPlanner.Application.Interfaces;
using iPlanner.Application.Interfaces.Repository;
using iPlanner.Infrastructure.Common;

namespace iPlanner.Infrastructure.Repositories.Orders
{
    public class FileOrderRepository : IOrderRepository
    {
        private readonly IFileService _fileService;
        private readonly string _ordersFilePath;

        public FileOrderRepository(IFileService fileService)
        {
            _fileService = fileService;
            _ordersFilePath = _fileService.GetDataFilePath("orders.json");
        }

        public async Task<OrderDTO> GetOrderByIdAsync(string id)
        {
            var orders = await LoadOrdersAsync();
            return orders.FirstOrDefault(o => o.Id.Equals(id));
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await LoadOrdersAsync();
        }

        public async Task UpdateOrderAsync(OrderDTO order)
        {
            var orders = await LoadOrdersAsync();
            var existingOrder = orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder != null)
            {
                orders.Remove(existingOrder);
            }
            orders.Add(order);
            await SaveOrdersAsync(orders);
        }

        private async Task<List<OrderDTO>> LoadOrdersAsync()
        {
            return await Task.Run(() => _fileService.LoadJsonData<List<OrderDTO>>(_ordersFilePath) ?? new List<OrderDTO>());
        }

        private async Task SaveOrdersAsync(List<OrderDTO> orders)
        {
            await Task.Run(() => _fileService.SaveJsonData(_ordersFilePath, orders));
        }

        public Task UpdateOrdersAsync(IEnumerable<OrderDTO> orders)
        {
            return SaveOrdersAsync(orders.ToList());
        }

        public async Task CreateOrderAsync(OrderDTO order)
        {
            var orders = await LoadOrdersAsync();
            if (order.Pending)
            {
                order.Id = IdGenerator.GenerateUUID();
            }
            orders.Add(order);
            await SaveOrdersAsync(orders);
        }
    }
}
