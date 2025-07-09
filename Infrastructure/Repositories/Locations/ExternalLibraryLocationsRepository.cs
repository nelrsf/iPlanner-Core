using iPlanner.Application.DTO;
using iPlanner.Application.Interfaces.Repository;
using iPlanner.Application.Mappers;
// TODO: Re-enable when LocationsTools dependency is available
//using LocationsTools.src.Controllers;
//using LocationsTools.src.Model.Equipments;
//using LocationsTools.src.Model.Locations;

namespace iPlanner.Infrastructure.Locations
{
    // TODO: Re-enable when LocationsTools dependency is available
    // Temporary stub implementation to allow compilation
    public class ExternalLibraryLocationsRepository : ILocationsRepository
    {
        public async Task<ICollection<LocationItemsDTO>> GetLocations()
        {
            return new List<LocationItemsDTO>();
        }
    }
}