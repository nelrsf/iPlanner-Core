using iPlanner.Core.Application.DTO.Orders;

namespace iPlanner.Core.Application.Interfaces.Repository
{
    public interface IOrdersRetriever
    {
        Task<IEnumerable<OrderDTO>> GetOrders();
    }
}

