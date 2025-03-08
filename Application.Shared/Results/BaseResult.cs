
namespace Application.Shared.Results
{
    public class BaseResult
    {
        protected string error;
        protected bool result;

        public  BaseResult()
        {
            result = true;
            error = "";
        }

        public BaseResult(string error)
        {
            result = false;
            this.error = error;
        }

        public static BaseResult FailureResult(string error)
        {
            return new BaseResult(error);
        }

        public static BaseResult SuccessResult()
        {
            return new BaseResult();
        }

        public bool IsSuccess { get => result == true; }

        public bool IsFailure { get => result == false; }
    
        public string? Error
        {
            get => result ? null : error;
        }
    }
}
