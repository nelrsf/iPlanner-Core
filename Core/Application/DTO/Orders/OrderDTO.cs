namespace iPlanner.Core.Application.DTO.Orders
{
    public class OrderDTO
    {
        public string? Id { get; set; }
        public string? Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public OrderClass Class { get; set; } = OrderClass.P01;
        public NotificationType Notification { get; set; } = NotificationType.Y4;

        public bool Pending { get; set; } = false;

        public string? SAPTechnicalLocation { get; set; }
        public string? SAPEquipment { get; set; }

        public ICollection<ReportsDTO>? Reports { get; set; }

    }

    public enum NotificationType
    {
        Y2, Y3, Y4, Y5
    }

    public enum OrderClass
    {
        P01, N02, N03, P04, P06, C01, A02
    }
}
