namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        // Details will contain thr stack trace of 500 errors
        public string Details { get; set; }
    }
}