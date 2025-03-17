namespace Test.Application.Common.Interfaces
{
    public interface ITestAccessRequest
    {
        public long TestId { get; set; }    

        public string StudentEmail { get; set; }
    }
}
