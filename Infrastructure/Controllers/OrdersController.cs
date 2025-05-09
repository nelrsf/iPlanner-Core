using iPlanner.Core.Application.DTO.Orders;
using iPlanner.Core.Application.Services.Orders;
using Microsoft.AspNetCore.Mvc;

namespace iPlanner.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Obtiene todos los pedidos.
        /// </summary>
        /// <returns>Una colección de pedidos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Obtiene un pedido específico por su ID.
        /// </summary>
        /// <param name="id">El ID del pedido.</param>
        /// <returns>El pedido correspondiente al ID proporcionado.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        /// <summary>
        /// Crea un nuevo pedido pendiente.
        /// </summary>
        /// <param name="order">El pedido a crear.</param>
        [HttpPost]
        public async Task<IActionResult> CreatePendingOrder([FromBody] OrderDTO order)
        {
            await _orderService.CreatePendingOrderAsync(order);
            return Ok();
        }

        /// <summary>
        /// Actualiza un pedido existente.
        /// </summary>
        /// <param name="order">El pedido a actualizar.</param>
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDTO order)
        {
            await _orderService.UpdateOrderAsync(order);
            return Ok();
        }

        /// <summary>
        /// Obtiene los reportes asociados a un pedido.
        /// </summary>
        /// <param name="order">El pedido para el cual se obtendrán los reportes.</param>
        /// <returns>Una colección de reportes asociados al pedido.</returns>
        [HttpPost("reports")]
        public async Task<IActionResult> GetReportsByOrder([FromBody] OrderDTO order)
        {
            var reports = await _orderService.GetReportsByOrder(order);
            return Ok(reports);
        }
    }
}
