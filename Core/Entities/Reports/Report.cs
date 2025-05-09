using iPlanner.Core.Entities.Interfaces;
using iPlanner.Core.Entities.Locations;
using iPlanner.Core.Entities.Teams;

namespace iPlanner.Core.Entities.Reports
{
    public class Report : IClonable<Report>
    {
        private double _totalHours;
        private Team? _team;
        public string? ReportId { get; set; }
        public Order? Order { get; set; }
        public Team? Team
        {
            get { return _team; }
            set
            {
                _team = value;
            }
        }

        private DateTime? _date;

        public ReportCustomTeam CustomTeam { get; }

        public double TotalHours
        {
            get
            {
                if (TimeInit.HasValue && TimeEnd.HasValue)
                {
                    return (TimeEnd.Value - TimeInit.Value).TotalHours;
                }
                return 0;
            }
        }


        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                ReportScheduleUpdater.UpdateScheduleByDate(this);
            }
        }

        public int WeekNumber
        {
            get
            {
                if (Date.HasValue)
                {
                    var culture = System.Globalization.CultureInfo.CurrentCulture;
                    return culture.Calendar.GetWeekOfYear(Date.Value, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                }
                return 0;
            }
        }

        public TimeSpan? TimeInit { get; set; }

        public TimeSpan? TimeEnd { get; set; }

        public ICollection<Activity>? Activities { get; set; }

        public Report()
        {
            CustomTeam = new ReportCustomTeam(Team);
        }

        public void UpdateTeamMembers()
        {
            CustomTeam.UpdateActiveMembers(Team);
        }

        public Report Clone()
        {
            var clonedReport = new Report
            {
                Order = Order,
                Team = Team,
                Date = Date,
                TimeInit = TimeInit,
                TimeEnd = TimeEnd,
                Activities = Activities?.Select(a => new Activity
                {
                    Description = a.Description,
                    Locations = a.Locations.Select(l => new LocationItem(l.LocationId, l._name, l.Icon, l.LocationType)).ToList()
                }).ToList()
            };
            clonedReport.CustomTeam.CopyActiveMembers(this);
            return clonedReport;
        }


        public bool HasOvertime()
        {
            TimeSpan? overtimeLowerLimit = ReportScheduleUpdater.GetTimeInit(Date.Value.DayOfWeek);
            TimeSpan? overtimeUpperimit = ReportScheduleUpdater.GetTimeEnd(Date.Value.DayOfWeek);
            if (TimeInit == null || TimeEnd == null) return false;
            if (overtimeLowerLimit == null || overtimeLowerLimit == null) return true;

            else if (TimeInit < overtimeLowerLimit && TimeEnd <= overtimeLowerLimit)
            {
                return true;
            }
            else if (TimeEnd > overtimeUpperimit && TimeInit >= overtimeUpperimit)
            {
                return true;
            }
            else if (TimeInit >= overtimeLowerLimit && TimeEnd <= overtimeUpperimit)
            {
                return false;
            }
            else
            {
                throw new Exception($"Error, tiempo extra y tiempo ordinario traslapados en el mismo reporte\nEquipo: {Team?.Name}, Fecha: {Date.ToString()}");
            }

        }
    }

    public class Activity
    {
        public ICollection<LocationItem> Locations { get; set; }
        public string? Description { get; set; }
        public Activity()
        {
            Locations = new List<LocationItem>();
        }
    }


    public class ReportCustomTeam
    {
        public class ReportCustomMember
        {
            public bool IsActive { get; set; }
            public TeamMember TeamMember { get; set; }

            public ReportCustomMember(TeamMember teamMember)
            {
                TeamMember = teamMember;
            }

            public ReportCustomMember(bool isActive, TeamMember teamMember)
            {
                IsActive = isActive;
                TeamMember = teamMember;
            }
        }


        public List<ReportCustomMember> ActiveMembers { get; set; }

        public ReportCustomTeam(Team team)
        {
            ActiveMembers = new List<ReportCustomMember>();
            UpdateActiveMembers(team);
        }

        public void UpdateActiveMembers(Team team)
        {
            if (team == null) return;
            if (team.Members == null) return;
            //if (!TeamHasChanged(team)) return;
            ActiveMembers.Clear();
            foreach (var member in team.Members)
            {
                ActiveMembers.Add(new ReportCustomMember(true, member));
            }
        }

        public void ToggleMember(TeamMember member)
        {
            var customMember = ActiveMembers.FirstOrDefault(m => m.TeamMember == member);
            if (customMember != null)
            {
                customMember.IsActive = !customMember.IsActive;
            }
        }


        public void CopyActiveMembers(Report report)
        {
            ActiveMembers.Clear();
            ActiveMembers = new List<ReportCustomMember>();
            foreach (var member in report.CustomTeam.ActiveMembers)
            {
                ActiveMembers.Add(new ReportCustomMember(member.IsActive, member.TeamMember));
            }
        }

    }
}
