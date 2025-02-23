namespace Application.Shared.Exceptions
{
    public class BaseApiException : Exception
    {
        public BaseApiException(
            string message) : base(message) { }
    }
}
