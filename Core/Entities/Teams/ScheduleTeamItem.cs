using iPlanner.Core.Entities.Reports;

namespace iPlanner.Core.Entities.Teams
{
    public class ScheduleTeamItem
    {
        public string Id { get; set; }
        public string Name { get; set; }


        private double totalHours = 0;
        public double TotalHours
        {
            get => totalHours;
            set => totalHours = value;
        }

        public double CompletionPercentage
        {
            get
            {
                double requiredTotalHours = ReportScheduleUpdater.GetRequieredTotalHours();
                return totalHours / requiredTotalHours * 100;
            }
        }
        public bool HasConflicts { get; set; }

        public ScheduleTeamItem(Team team)
        {
            Id = team.Id;
            Name = team.Name;
        }
    }
}
