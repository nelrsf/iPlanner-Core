using iPlanner.Core.Entities.Reports;

namespace iPlanner.Core.Entities.Teams.ConflictSchedule
{
    public class MemberHasOverlappingTime : IConflictScheduleRule
    {
        public IEnumerable<ConflictItem> EvaluateConflicts(Report report, List<Report> reports)
        {
            var members = report.CustomTeam.ActiveMembers.Where(am => am.IsActive).ToList();
            List<TeamMember> activeMembers = members.Select(m => m.TeamMember).ToList();
            foreach (var member in activeMembers)
            {
                IEnumerable<Report> relatedReports = reports.Where(r => r.CustomTeam.ActiveMembers.Any(am=>am.IsActive && am.TeamMember.Id == member.Id));
                IEnumerable<Report> reportsWithSameDate = relatedReports.Where(r => r.Date == report.Date);
                IEnumerable<Report> reportsWithOverlappingTime = reportsWithSameDate.Where(r => HasTimeOverlap(r, report));
                if (reportsWithOverlappingTime.Any())
                {
                    yield return new ConflictItem
                    {
                        Team = report.Team,
                        Date = report.Date.Value,
                        Description = $"Conflicto en los reportes del trabajador {member.Name ?? "Desconocido"}, el dia {report.Date.Value}"
                    };
                }
            }

        }

        private bool HasTimeOverlap(Report r, Report report)
        {
            if (r.TimeInit == null || r.TimeEnd == null || report.TimeInit == null || report.TimeEnd == null)
            {
                return false;
            }

            return r.TimeInit < report.TimeEnd && report.TimeInit < r.TimeEnd;
        }
    }
}
