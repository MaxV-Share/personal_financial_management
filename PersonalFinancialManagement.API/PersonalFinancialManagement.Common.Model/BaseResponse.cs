namespace PersonalFinancialManagement.Common.Models
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {

        }
        public BaseResponse(int statusCode, string message, T body = default(T))
        {
            StatusCode = statusCode;
            Message = message;
            Body = body;
        }
        public int StatusCode { get; set; }
        public T Body { get; set; }
        public string Message { get; set; }
    }
}
