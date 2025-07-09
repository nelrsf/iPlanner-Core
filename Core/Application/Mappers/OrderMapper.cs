using iPlanner.Application.DTO.Orders;
using iPlanner.Application.Interfaces;
using iPlanner.Entities.Reports;

namespace iPlanner.Application.Mappers
{
    public class OrderMapper : IMapper<OrderDTO, Order>
    {
        public OrderDTO ToDTO(Order entity)
        {
            if (entity == null)
            {
                return (OrderDTO)null;
            }
            return new OrderDTO
            {
                Id = entity.Id,
                EndDate = entity.EndDate,
                StartDate = entity.StartDate,
                Text = entity.Text
            };
        }

        public Order ToEntity(OrderDTO dto)
        {
            if (dto == null)
            {
                return (Order)null;
            }
            return new Order
            {
                Id = dto.Id,
                EndDate = dto.EndDate,
                StartDate = dto.StartDate,
                Text = dto.Text
            };
        }
    }
}
