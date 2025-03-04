namespace Test.Contracts.Student.Requests
{
    public class CreateStudentRequest
    {
        public int GroupNumber { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}
