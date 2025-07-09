namespace iPlanner.Application.Interfaces
{
    public interface IMapper<DTO, Entity> where DTO : class where Entity : class
    {
        public DTO ToDTO(Entity entity);
        public Entity ToEntity(DTO dto);
    }
}
