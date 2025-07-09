using iPlanner.Entities.Reports;
using iPlanner.Entities.Teams;
using iPlanner.Entities.Teams.ConflictSchedule;

namespace iPlanner.Application.Services.Teams
{

    public class ConflictValidationService : IConflictValidationService
    {
        private readonly List<IConflictScheduleRule> _conflictRules;

        public ConflictValidationService()
        {
            _conflictRules = new List<IConflictScheduleRule>();
            _conflictRules.Add(new OverlappingReportsRule());
            _conflictRules.Add(new OverlappingTimeLimitsRule());
            _conflictRules.Add(new MemberHasOverlappingTime());
        }

        public void ComputeConflicts(ScheduleTeamData scheduleTeamData, List<Report> reports)
        {
            var conflictItems = new List<ConflictItem>();

            foreach (var report in reports.Where(r => r.Date.HasValue))
            {
                foreach (var rule in _conflictRules)
                {
                    var conflicts = rule.EvaluateConflicts(report, reports);
                    conflictItems.AddRange(conflicts);
                }
            }

            if (conflictItems.Any())
            {
                scheduleTeamData.ConflictItems = conflictItems;
                MarkTeamsWithConflicts(scheduleTeamData, conflictItems);
            }
        }

        private void MarkTeamsWithConflicts(ScheduleTeamData scheduleTeamData, List<ConflictItem> conflictItems)
        {
            foreach (var teamItem in scheduleTeamData.TeamItems)
            {
                if (conflictItems.Any(c => c.Team.Id == teamItem.Id))
                    teamItem.HasConflicts = true;
            }
        }
    }


}
