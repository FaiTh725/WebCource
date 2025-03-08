namespace Test.Application.Events.Teacher
{
    public class SuccessCreateTeacher
    {
        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int GroupName { get; set; }
    }
}
