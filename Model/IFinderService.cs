namespace Model
{
    public interface IFinderService
    {
        Task<Dictionary<ulong, List<string>>> FindDuplicatesAsync();
        void SetName(string name);
        event EventHandler<GreetingArgs> OnGreetingChanged;

        string Name { get; }
    }
}