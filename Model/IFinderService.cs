namespace Model
{
    public interface IFinderService
    {
        void FindDuplicates();
        void SetName(string name);
        event EventHandler<GreetingArgs> OnGreetingChanged;

        string Name { get; }
    }
}