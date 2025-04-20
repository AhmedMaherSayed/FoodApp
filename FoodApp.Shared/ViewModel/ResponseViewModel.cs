using FoodApp.Domain.Data.Enums;

namespace FoodApp.Presentation.ViewModel
{
    public class ResponseViewModel<T>
    {

        public T Data { get; set; }
        public bool IsSucsess { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string Message { get; set; }

        public static ResponseViewModel<T> Success(T Data)
        {
            return new ResponseViewModel<T>
            {
                Data = Data,
                IsSucsess = true,
                Message = "Request completed sucsessfully",
                ErrorCode = ErrorCode.None
            };
        }
        public static ResponseViewModel<T> Failure(ErrorCode errorCode, string message = "")
        {
            return new ResponseViewModel<T>
            {
                Data = default,
                IsSucsess = false,
                Message = message,
                ErrorCode = errorCode
            };
        }

    }
}
