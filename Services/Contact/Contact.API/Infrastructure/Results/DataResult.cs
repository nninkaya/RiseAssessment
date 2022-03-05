namespace Contact.API.Infrastructure.Results
{
    #region INTERFACE
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
    #endregion


    #region CLASS
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
    #endregion
}
