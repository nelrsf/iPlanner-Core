using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Orders;
using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Interfaces.Repository;

namespace iPlanner.Core.Application.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrdersRetriever _ordersRetriever;
        private readonly IReportService _reportService;

        public OrderService(IOrderRepository orderRepository, IOrdersRetriever ordersRetriever, IReportService reportService)
        {
            _orderRepository = orderRepository;
            _ordersRetriever = ordersRetriever;
            _reportService = reportService;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(string id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllOrdersAsync();
                var existingOrders = orders.ToList();
                var retreivedOrders = await _ordersRetriever.GetOrders();
                foreach (var order in retreivedOrders)
                {
                    if (!orders.Contains(order))
                    {
                        existingOrders.Add(order);
                    }
                }

                return existingOrders;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }

        }

        public async Task UpdateOrderAsync(OrderDTO order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task CreatePendingOrderAsync(OrderDTO order)
        {
            await _orderRepository.CreateOrderAsync(order);
        }

        public async Task<ICollection<ReportsDTO>> GetReportsByOrder(OrderDTO orderDTO)
        {
            var filter = new ReportFilterDTO
            {
                Order = orderDTO
            };
            var reports = await _reportService.GetReportsAsyncByFilter(filter);
            return reports;

        }

    }
}
