namespace iPlanner.Application.DTO.Teams
{
    public class ScheduleTeamDataDTO
    {
        public IEnumerable<ScheduleTeamItemDTO> TeamItems { get; set; }
        public IEnumerable<ConflictItemDTO> ConflictItems { get; set; }
    }

    public class ScheduleTeamItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double TotalHours { get; set; }
        public double CompletionPercentage { get; set; }
        public bool HasConflicts { get; set; }
    }

    public class ConflictItemDTO
    {
        public TeamDTO Team { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
