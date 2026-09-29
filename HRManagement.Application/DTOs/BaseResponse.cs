

namespace hrmana.HRManagement.Application.DTOs
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public static BaseResponse<T> Ok(T Data,string Message ="Netflix con vo oi")
        {
            return new BaseResponse<T>
            {
                Success = true,
                Message = Message,
                Data = Data
            };
        }
        public static BaseResponse<T> Fail( string Message = "gadayumm not good bruh ", List<string> errors = null)
        {
            return new BaseResponse<T>
            {
                Success = false,
                Message = Message,
                Errors = errors
            };
        }

    }
}
