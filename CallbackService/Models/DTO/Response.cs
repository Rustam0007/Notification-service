using CallbackService.Enums;

namespace CallbackService.Models.DTO;


public class Response<T>
{
    public Response()
    {
    }

    public Response(int code, string errorMassage)
    {
        Code = code;
        Message = errorMassage;
    }

    public int Code { get; set; }
    public string Message { get; set; }
    public T Payload { get; set; }

    public static Response<T> FailResponse(Errors errorCode, string errorMessage = null)
    {
        return new()
        {
            Code = (int) errorCode,
            Message = errorMessage ?? errorCode.GetDescription(),
            Payload = default
        };
    }

    public static Response<T> SuccessResponse(Errors errorCode, T payload)
    {
        return new()
        {
            Code = (int) errorCode,
            Message = errorCode.GetDescription(),
            Payload = payload
        };
    }
}
