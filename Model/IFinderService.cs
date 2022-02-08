namespace Model
{
    public interface IFinderService
    {
        Task<Dictionary<ulong, List<string>>> FindDuplicatesAsync();
    }
}