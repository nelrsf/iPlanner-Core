using iPlanner.Core.Entities.Reports;

namespace iPlanner.Core.Entities.Teams.ConflictSchedule
{
    public class OverlappingReportsRule : IConflictScheduleRule
    {
        public IEnumerable<ConflictItem> EvaluateConflicts(Report report, List<Report> reports)
        {
            var overlappingReports = reports.Where(r =>
                HasSameDate(r, report) &&
                HasSameActiveMembers(r, report) &&
                IsNotSameReport(r, report) &&
                IsFromSameTeam(r, report) &&
                HasTimeOverlap(r, report));

            foreach (var overlap in overlappingReports)
            {
                yield return new ConflictItem
                {
                    Team = report.Team,
                    Date = report.Date.Value,
                    Description = $"Conflicto en los reportes del frente {report.Team.Name ?? "Desconocido"}, el dia {report.Date.Value}"
                };
            }
        }

        private bool HasSameActiveMembers(Report r, Report report)
        {
            var activeMembers = r.CustomTeam.ActiveMembers.Where(acvMem => acvMem.IsActive);
            foreach(var member in report.CustomTeam.ActiveMembers.Where(acvMem => acvMem.IsActive))
            {
                if (!activeMembers.Any(am => am.TeamMember.Id == member.TeamMember.Id))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool HasSameDate(Report r1, Report r2)
        {
            if (r1 == null || r2 == null)
            {
                return false;
            }
            return r1.Date == r2.Date;
        }

        private static bool IsNotSameReport(Report r1, Report r2)
        {
            if (r1.Team == null || r2.Team == null)
            {
                return false;
            }
            return r1.ReportId != r2.ReportId;
        }

        private static bool IsFromSameTeam(Report r1, Report r2) {
            if(r1.Team == null || r2.Team == null)
            {
                return false;
            }
            return r1.Team.Id == r2.Team.Id;
        }


        private static bool HasTimeOverlap(Report r1, Report r2) =>
            (r1.TimeInit >= r2.TimeInit && r1.TimeInit < r2.TimeEnd) ||    // Inicio dentro del rango
            (r1.TimeEnd > r2.TimeInit && r1.TimeEnd <= r2.TimeEnd) ||      // Fin dentro del rango
            (r1.TimeInit <= r2.TimeInit && r1.TimeEnd >= r2.TimeEnd);      // Abarca todo el rango
    }
}
