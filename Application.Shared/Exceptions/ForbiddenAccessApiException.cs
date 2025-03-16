namespace Application.Shared.Exceptions
{
    public class ForbiddenAccessApiException : 
        BaseApiException
    {

        public ForbiddenAccessApiException(string message) : 
            base(message)
        {
            
        }
    }
}
