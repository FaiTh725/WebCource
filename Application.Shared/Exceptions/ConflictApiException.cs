namespace Application.Shared.Exceptions
{
    public class ConflictApiException : BaseApiException
    {
        public ConflictApiException(string message) : 
            base(message)
        {
            
        }
    }
}
