
using CSharpFunctionalExtensions;
using Test.Domain.Event;
using Test.Domain.Primitives;

namespace Test.Domain.Entities
{
    public class TestAttempt : DomainEventEntity
    {
        public Student AnswerStudent { get; init; }

        public Test Test { get; init; }

        public List<TestAnswer> Answers { get; init; }

        public double Percent { get; set; }

        public DateTime AnswerDate { get; init; }

        // For EF
        public TestAttempt() { }

        private TestAttempt(
            Student answer,
            Test test,
            List<TestAnswer> answers,
            double percent)
        {
            AnswerStudent = answer;
            Test = test;
            Percent = percent;
            Answers = answers;

            AnswerDate = DateTime.UtcNow;

            RaiseDomainEvent(new TestCompletedEvent
            {
                StudentId = AnswerStudent.Id,
                TestId = Test.Id
            });
        }

        public static Result<TestAttempt> Initialize(
            Student answer,
            Test test,
            List<TestAnswer> answers,
            double percent)
        {
            if (answer is null ||
                test is null)
            {
                return Result.Failure<TestAttempt>("Answer Or Test is null");
            }

            if(percent < 0 || 
                percent > 100)
            {
                return Result.Failure<TestAttempt>("Percent is out of 0 and 100");
            }

            if(answers is null)
            {
                return Result.Failure<TestAttempt>("Attemp has no answers");
            }

            return Result.Success(new TestAttempt(
                answer,
                test,
                answers,
                percent));
        }
    }
}
