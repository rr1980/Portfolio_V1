namespace RR.ExceptionHandler
{
    internal class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public string Message { get; set; }
        public string Place { get; set; }
        public string StackTrace { get; set; }
    }
}