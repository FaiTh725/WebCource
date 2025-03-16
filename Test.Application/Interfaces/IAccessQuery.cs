namespace Test.Application.Interfaces
{
    public interface IAccessQuery
    {
        public string Email { get; }

        public string Role { get; }

        public long AttemptId { get; }
    }
}
