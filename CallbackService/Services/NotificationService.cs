using CallbackService.Enums;
using CallbackService.Models;
using CallbackService.Models.DTO;
using CallbackService.Repository;
using Newtonsoft.Json;

namespace CallbackService.Services;
public class NotificationService
{
    private readonly IDatabaseService _db;
    public NotificationService(IDatabaseService db)
    {
        _db = db;
    }

    public Response<NotificationResponse> Create(NotificationRequest request)
    {
        var response = new Response<NotificationResponse>();
        var messageJson = JsonConvert.SerializeObject(request);
        try
        {
            var notificationId = _db.InsertNotification(request.CardId, messageJson);

            response.Code = (int) Errors.Approved;
            response.Message = Errors.Approved.GetDescription();
            response.Payload = new NotificationResponse
            {
                Id = notificationId
            };
        }
        catch (Exception e)
        {
            response.Code = (int) Errors.InternalError;
            response.Message = Errors.InternalError.GetDescription();
        }
        return response;
    }
}