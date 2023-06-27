using CallbackService.Models;
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
    public NotificationRes Create([FromBody] NotificationReq notificationReq)
    {
        return _notificationService.Create(notificationReq);
    }
}