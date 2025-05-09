using iPlanner.Core.Application.DTO.Orders;
using iPlanner.Core.Application.DTO.Teams;

namespace iPlanner.Core.Application.DTO.Reports
{
    public class ReportFilterDTO
    {
        public OrderDTO? Order { get; set; }
        public DateTime? DateInit { get; set; }

        public DateTime? DateEnd { get; set; }

        public TeamDTO? Team { get; set; }

    }
}
