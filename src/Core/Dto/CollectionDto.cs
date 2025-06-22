namespace Core.Dto;

public class CollectionDto<T> where T : class
{
    public required IEnumerable<T> Values { get; set; }
    public required int TotalPages { get; set; }
}
