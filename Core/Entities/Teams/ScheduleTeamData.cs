namespace iPlanner.Core.Entities.Teams
{
    public class ScheduleTeamData
    {
        public IEnumerable<ScheduleTeamItem> TeamItems { get; set; }
        public IEnumerable<ConflictItem> ConflictItems { get; set; }

        public ScheduleTeamItem GetTeamScheduleItem(Team team) {
            ScheduleTeamItem? item = TeamItems.FirstOrDefault(item => item.Id == team.Id);
            if (item == null)
            {
                item = new ScheduleTeamItem(team);
                ((List<ScheduleTeamItem>)TeamItems).Add(item);
            }
            return item;
        }
    }
}
