using CallbackService.Enums;

namespace CallbackService.Models.DTO;


public class Response<T>
{
    public Response()
    {
    }

    public int Code { get; set; }
    public string Message { get; set; }
    public T Payload { get; set; }
}
