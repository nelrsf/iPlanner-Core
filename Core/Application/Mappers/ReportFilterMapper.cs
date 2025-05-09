using iPlanner.Core.Application.DTO.Orders;
using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Reports;
using iPlanner.Core.Entities.Teams;

namespace iPlanner.Core.Application.Mappers
{
    class ReportFilterMapper : IMapper<ReportFilterDTO, ReportFilter>
    {
        private IMapper<TeamDTO, Team> _teamMapper;
        private IMapper<OrderDTO, Order> _orderMapper;
        public ReportFilterMapper(IMapper<TeamDTO, Team> teamMapper, IMapper<OrderDTO, Order> orderMapper) {
            _teamMapper = teamMapper;
            _orderMapper = orderMapper;
        }
        public ReportFilterDTO ToDTO(ReportFilter entity)
        {
            return new ReportFilterDTO
            {
                DateEnd = entity.DateEnd,
                Order = _orderMapper.ToDTO(entity.Order ?? new Order()),
                DateInit = entity.DateInit,
                Team = _teamMapper.ToDTO(entity.Team ?? new Team())
            };
        }

        public ReportFilter ToEntity(ReportFilterDTO dto)
        {
            return new ReportFilter
            {
                DateEnd = dto.DateEnd,
                DateInit = dto.DateInit,
                Order = _orderMapper.ToEntity(dto.Order),
                Team = _teamMapper.ToEntity(dto.Team)
            };
        }
    }
}
