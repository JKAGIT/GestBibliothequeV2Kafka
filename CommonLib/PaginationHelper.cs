public static class PaginationHelper
{
    public static List<T> Paginate<T>(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        return source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }
}
