using iPlanner.Application.DTO;
using iPlanner.Application.DTO.Orders;

namespace iPlanner.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrderByIdAsync(string id);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task UpdateOrderAsync(OrderDTO order);
        Task CreatePendingOrderAsync(OrderDTO order);
        Task<ICollection<ReportsDTO>> GetReportsByOrder(OrderDTO orderDTO);
    }
}
