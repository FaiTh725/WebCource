namespace Test.Contracts.Student.Responses
{
    public class StudentCreatedResponse
    {
        public bool IsSuccess { get; init; }

        public string ErrorMessage { get; init; } = string.Empty;
    }
}
