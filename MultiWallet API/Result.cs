namespace _result
{
    public class Result <T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public Result(bool isSuccess, T? data, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }
        public static Result<T> Success(T data) => new Result<T>(true, data, null);
        public static Result<T> Error(string errorMessage) => new Result<T>(false, default, errorMessage);
    }
}
