using iPlanner.Application.DTO.Orders;

namespace iPlanner.Application.Interfaces.Repository
{
    public interface IOrdersRetriever
    {
        Task<IEnumerable<OrderDTO>> GetOrders();
    }
}

