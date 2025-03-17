namespace Test.Application.Interfaces
{
    public interface ITestAccessService
    {
        Task<bool> HasAccess(long testId, string studentEmail);
    }
}
