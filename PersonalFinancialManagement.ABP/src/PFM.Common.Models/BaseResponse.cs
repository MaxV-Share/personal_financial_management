using PFM;
namespace PFM.Common.Models
{
    public class BaseResponse<T> where T : class
    {
        public BaseResponse()
        {
            Body = default;
        }
        public BaseResponse(int statusCode, string message, T? body = default)
        {
            StatusCode = statusCode;
            Message = message;
            Body = body;
        }
        public int? StatusCode { get; set; }
        public T? Body { get; set; }
        public string? Message { get; set; }
    }
}
