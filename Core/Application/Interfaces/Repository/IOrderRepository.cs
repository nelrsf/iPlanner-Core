using iPlanner.Application.DTO;
using iPlanner.Application.DTO.Orders;

namespace iPlanner.Application.Interfaces.Repository
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