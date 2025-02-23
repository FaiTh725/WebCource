namespace Application.Shared.Exceptions
{
    public class NotFoundApiException : BaseApiException
    {
        public NotFoundApiException(string message) : 
            base(message)
        {
            
        }
    }
}
