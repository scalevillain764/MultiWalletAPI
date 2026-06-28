namespace _result
{
    public class Result <T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public Result(bool isSuccess, T? data, string? errorMessage, ErrorType? _errorType)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
            errorType = _errorType;
        }
        public enum ErrorType
        {
            Validation,
            Unauthorized,
            Forbidden,
            NotFound,
            Conflict
        }
        public ErrorType? errorType { get; set; }
        public static Result<T> Success(T data) => new Result<T>(true, data, null, null);
        public static Result<T> Error(string errorMessage, ErrorType? errorType) => new Result<T>(false, default, errorMessage, errorType);
    }
}
