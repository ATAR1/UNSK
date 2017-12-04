namespace TestAndTunes.Routines
{
    public interface IOption
    {
        string Description { get; }
        bool IsEnabled { get; set; }
    }
}