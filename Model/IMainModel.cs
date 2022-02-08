namespace Model
{
    public interface IMainModel
    {
        void FindDuplicates();
        void SetName(string name);
        event EventHandler<GreetingArgs> OnGreetingChanged;

        string Name { get; }
    }
}