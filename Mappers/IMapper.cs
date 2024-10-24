public interface IMapper<E,D,C>
{
    public E ToEntity(D dto);
    public D ToDto(E entity);
    public E toEntity(C createDto);

}
