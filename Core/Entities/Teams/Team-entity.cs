namespace iPlanner.Core.Entities.Teams
{
    public class TeamBase
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
    public class TeamMember : TeamBase
    {

        public string? Position { get; set; }
    }

    public class Team : TeamBase
    {
        public string? Description { get; set; }
        public TeamMember? Leader { get; set; }
        public ICollection<TeamMember>? Members { get; set; }

        public Team()
        {
            Members = new List<TeamMember>();
        }
    }
}
