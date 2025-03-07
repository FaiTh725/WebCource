namespace Application.Shared.Results
{
    public class DataResult<T> : BaseResult
    {
        private readonly T? value;

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

        public T Value
        {
            get
            {
                if (!result)
                {
                    throw new InvalidOperationException("Get Value When Result Is Failure");
                }

                return value!;
            }
        }
    }
}
