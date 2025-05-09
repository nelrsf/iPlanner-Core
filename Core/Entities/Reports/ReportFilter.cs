using iPlanner.Core.Entities.Teams;

namespace iPlanner.Core.Entities.Reports
{
    public class ReportFilter
    {
        public DateTime? DateInit { get; set; }

        public DateTime? DateEnd { get; set; }

        public Order? Order { get; set; }

        public Team? Team { get; set; }

        public virtual bool CheckTime()
        {
            if (DateInit == null) return true;
            if (DateEnd == null) return true;
            if (DateEnd == DateInit) return true;
            return DateInit < DateEnd;
        }

        internal List<Report> FilterReports(List<Report> reports)
        {
            return reports
                .Where(r => r.Date >= DateInit || DateInit == null)
                .Where(r => r.Date <= DateEnd || DateEnd == null)
                .Where(r => r.Order?.Id == Order?.Id || Order == null)
                .Where(r => Team == null || r?.Team?.Id == Team?.Id)
                .ToList();
        }
    }
}
