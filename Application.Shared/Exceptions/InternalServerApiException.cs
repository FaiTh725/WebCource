namespace Application.Shared.Exceptions
{
    public class InternalServerApiException : BaseApiException
    {
        public InternalServerApiException(string message) : 
            base(message)
        {
            
        }
    }
}
