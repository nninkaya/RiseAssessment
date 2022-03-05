namespace Contact.API.Infrastructure.Results
{
    #region INTERFACE
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
    #endregion

    #region CLASS
    public class Result : IResult
    {
        public bool Success { get; }
        public string? Message { get; }

        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }
    }
    #endregion
}
