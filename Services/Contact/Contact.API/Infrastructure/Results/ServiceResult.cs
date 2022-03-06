namespace Contact.API.Infrastructure.Results
{
    public class ServiceResult
    {
        public const int RESULT_SUCCESS = 0;
        public const int RESULT_UNSUCCESS = 1;
        public const int RESULT_DATAEXCEPTION = 2;
        public const int RESULT_EXCEPTION = 3;
        public const int RESULT_AUTHENTICATION = 4;

        public int ResultCode { get; set; }

        public string? ResultMessage { get; set; }
    }
}
