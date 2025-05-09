using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Orders;

namespace iPlanner.Core.Application.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(OrderDTO order);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(string id);
        Task UpdateOrderAsync(OrderDTO order);
        Task UpdateOrdersAsync(IEnumerable<OrderDTO> orders);
    }
}