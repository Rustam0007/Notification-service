using CallbackService.Models;
using CallbackService.Repository;
using Newtonsoft.Json;

namespace CallbackService.JobManager;

public class NotificationSendJob : IHostedService
{
    private readonly IDatabaseService _db;
    private readonly ILogger<NotificationSendJob> _logger;
    private readonly ISmsClient _smsClient;
    private Timer _timer;
    
    
    public NotificationSendJob(ILogger<NotificationSendJob> logger, IDatabaseService db, ISmsClient smsClient)
    {
        _db = db;
        _smsClient = smsClient;
        _logger = logger;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{GetType().Name} starting with interval of 2 seconds");
        _timer = new Timer(Work, null, 2000, Timeout.Infinite); //timer will fire 2 seconds after app start
        return Task.CompletedTask;
    }

    private void Work(object state)
    {
        try
        {
            var notifications = _db.GetUnsendNotification();
            foreach (var notification in notifications)
            {
                var messageJson = JsonConvert.DeserializeObject<NotificationRequest>(notification.Message);
                
                _smsClient.SendByPhoneNumber(notification.Phone, messageJson.EventDate, messageJson.OrderType, 
                    messageJson.Card, messageJson.WebsiteUrl);
               
                _db.UpdateNotificationStatus(notification.Id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while checking the notification table.");
        }
        finally
        {
            _logger.LogInformation($"Finished a cycle");
            _timer.Change(10000, Timeout.Infinite);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{GetType().Name} stopping");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}