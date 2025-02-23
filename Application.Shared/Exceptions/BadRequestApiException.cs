namespace Application.Shared.Exceptions
{
    public class BadRequestApiException : BaseApiException
    {
        public BadRequestApiException(string message) : 
            base(message)
        {
            
        }
    }
}
