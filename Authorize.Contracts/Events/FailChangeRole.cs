namespace Authorize.Contracts.Events
{
    public class FailChangeRole
    {
        public string Email { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;
    }
}
