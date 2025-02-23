namespace Application.Shared.Exceptions
{
    public class AppConfigurationException : Exception
    {
        public string SectionWithError { get; set; } = string.Empty;

        public AppConfigurationException(
            string sectionWithError = "",
            string message = "") : 
            base(message)
        {
            SectionWithError = sectionWithError;
        }
    }
}
