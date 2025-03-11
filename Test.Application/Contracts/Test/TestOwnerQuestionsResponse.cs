using Test.Application.Contracts.Question;
using Test.Application.Contracts.Teacher;

namespace Test.Application.Contracts.Test
{
    public class TestOwnerQuestionsResponse
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;
    
        public required TeacherResponse Owner { get; set; }

        public List<QuestionResponse> Questions { get; set; } = new List<QuestionResponse>();
    }
}
