namespace Application.Shared.Results
{
    public class DataResult<T> : BaseResult
    {
        private T? value;

        public DataResult(T data)
            : base()
        {
            value = data;
        }

        public DataResult(string error)
            :base(error)
        {
            value = default;
        }

        public static DataResult<TRes> FailureResult<TRes>(string error)
        {
            return new DataResult<TRes>(error);
        }

        public static DataResult<TRes> SuccessResult<TRes>(TRes value)
        {
            return new DataResult<TRes>(value);
        }

        public T? Value
        {
            get => result ? value : default;
        }
    }
}
