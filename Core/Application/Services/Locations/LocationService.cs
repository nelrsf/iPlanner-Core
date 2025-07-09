using iPlanner.Application.DTO;
using iPlanner.Application.Interfaces.Repository;

namespace iPlanner.Application.Services.Locations
{
    public class LocationService : ILocationService
    {
        private ILocationsRepository _locationsRepository;

        public LocationService(ILocationsRepository locationsRepository)
        {
            _locationsRepository = locationsRepository;
        }

        public async Task<ICollection<LocationItemsDTO>> GetLocations()
        {
            return await _locationsRepository.GetLocations();
        }


    }
}
