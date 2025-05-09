namespace iPlanner.Core.Entities.Locations
{
    public enum LocationTypes
    {
        Substation,
        Network,
        Structure,
        Equipmen,
        TreeNode
    }

    public class LocationItem
    {
        public string _name { get; set; }

        public int LocationId { get; set; }
        public string Icon { get; set; }

        public LocationTypes LocationType { get; set; }


        public LocationItem(int id, string name, string icon, LocationTypes type)
        {
            _name = name;
            Icon = icon;
            LocationId = id;
            LocationType = type;
        }



        public ICollection<LocationItem> Children { get; set; } = new List<LocationItem>();

    }
}
