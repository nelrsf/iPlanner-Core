using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Orders;

namespace iPlanner.Core.Application.Interfaces
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
