using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces.Repository;
using iPlanner.Core.Application.Mappers;
using LocationsTools.src.Controllers;
using LocationsTools.src.Model.Equipments;
using LocationsTools.src.Model.Locations;

namespace iPlanner.Infrastructure.Locations
{
    public class ExternalLibraryLocationsRepository : ILocationsRepository
    {
        private List<MVNetwork>? networks;
        private List<LocationItemsDTO> locationItems;
        ILocationsController locationsController;

        public ExternalLibraryLocationsRepository()
        {
            locationsController = new LocationControllerEFC();
            locationItems = new List<LocationItemsDTO>();
        }

        public async Task<ICollection<LocationItemsDTO>> GetLocations()
        {
            if (locationsController == null)
            {
                return new List<LocationItemsDTO>();
            }

            networks = await locationsController.getMVNetworks();
            foreach (MVNetwork network in networks)
            {
                locationItems.Add(GetLocationItemByType(network));
            }

            return locationItems;
        }

        private LocationItemsDTO? GetLocationItemByType(MVNetwork network)
        {
            if (network == null)
            {
                return null;
            }
            LocationItemsDTO locationItem = new LocationItemsDTO(network.id, network.description, "transmission", LocationTypeDTO.network);

            if (network.Substations != null && network.Substations.Count > 0)
            {
                LocationItemsDTO substationsNode = new LocationItemsDTO(-1, "Subestaciones", "home-bolt", LocationTypeDTO.treeNode);
                substationsNode.Parent = locationItem;
                locationItem.Children.Add(substationsNode);
                foreach (Substation substation in network.Substations)
                {
                    LocationItemsDTO substationNode = GetLocationItemByType(substation);
                    substationNode.Parent = substationsNode;
                    substationsNode.Children.Add(substationNode);
                }
            }

            if (network.Structures != null && network.Structures.Count > 0)
            {
                LocationItemsDTO structuresNode = new LocationItemsDTO(-1, "Estructuras", "transmission", LocationTypeDTO.treeNode);
                structuresNode.Parent = locationItem;
                locationItem.Children.Add(structuresNode);
                foreach (Structure structure in network.Structures)
                {
                    LocationItemsDTO structureNode = GetLocationItemByType(structure);
                    structureNode.Parent = structuresNode;
                    structuresNode.Children.Add(structureNode);
                }
            }

            return locationItem;
        }

        private LocationItemsDTO GetLocationItemByType(Substation substation)
        {
            if (substation == null)
            {
                return null;
            }
            LocationItemsDTO locationItem = new LocationItemsDTO(substation.id, substation.description, "home-bolt", LocationTypeDTO.substation);

            if (substation.Equipments != null && substation.Equipments.Count > 0)
            {
                foreach (Equipment equipment in substation.Equipments)
                {
                    LocationItemsDTO equipmentNode = GetLocationItemByType(equipment);
                    equipmentNode.Parent = locationItem;
                    locationItem.Children.Add(equipmentNode);
                }
            }
            return locationItem;
        }

        private LocationItemsDTO GetLocationItemByType(Structure structure)
        {
            if (structure == null)
            {
                return null;
            }
            LocationItemsDTO locationItem = new LocationItemsDTO(structure.id, structure.description, "transmission", LocationTypeDTO.structure);
            if (structure.Equipments != null && structure.Equipments.Count > 0)
            {
                foreach (Equipment equipment in structure.Equipments)
                {
                    LocationItemsDTO equipmentNode = GetLocationItemByType(equipment);
                    equipmentNode.Parent = locationItem;
                    locationItem.Children.Add(equipmentNode);
                }
            }
            return locationItem;
        }

        private LocationItemsDTO GetLocationItemByType(Equipment equipment)
        {
            if (equipment == null)
            {
                return null;
            }
            LocationItemsDTO locationItem = new LocationItemsDTO(equipment.id, equipment.description, "switch", LocationTypeDTO.equipment);
            if (equipment.InnerEquipments != null && equipment.InnerEquipments.Count > 0)
            {
                foreach (Equipment equipment1 in equipment.InnerEquipments)
                {
                    LocationItemsDTO equipmentNode1 = GetLocationItemByType(equipment1);
                    equipmentNode1.Parent = locationItem;
                    locationItem.Children.Add(equipmentNode1);
                }
            }
            return locationItem;
        }
    }
}
