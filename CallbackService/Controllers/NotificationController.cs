using CallbackService.Models;
using CallbackService.Models.DTO;
using CallbackService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallbackService.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly NotificationService _notificationService;
    public NotificationController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("Create")]
    public Response<NotificationResponse> Create([FromBody] NotificationRequest request)
    {
        return _notificationService.Create(request);
    }
    
    [HttpGet("Get")]
    public Response<IEnumerable<NotificationJoin>> GetUnsend()
    {
        return _notificationService.GetFalse();
    }
}