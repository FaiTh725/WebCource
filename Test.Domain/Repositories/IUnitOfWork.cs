namespace Test.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public IStudentRepository StudentRepository { get; }
        public ITeacherRepository TeacherRepository { get; }
        public IGroupRepository GroupRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        public ITestRepository TestRepository { get; }
        public IQuestionRepository QuestionRepository { get; }
        public IQuestionVariantRepository QuestionVariantRepository { get; }
        public ITestAttemptRepository TestAttemptRepository { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();

        void BeginTransaction();
        Task BeginTransactionAsync();

        void CommitTransaction();
        Task CommiTransactionAsync();

        void RollBackTransaction();
        Task RollBackTransactionAsync();

        bool CanConnect();
        Task<bool> CanConnectAsync();
    }
}
