namespace Test.Application.Interfaces
{
    public interface ICachQuery
    {
        string Key { get; }

        int ExpirationSecond { get; }
    }
}
