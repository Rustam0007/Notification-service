using CallbackService.Models;
using CallbackService.Repository;
using Dapper;
using Newtonsoft.Json;
using Npgsql;

namespace CallbackService.JobManager;

public class NotificationSendJob : IHostedService
{
    private readonly IDatabaseService _db;
    private readonly ILogger<NotificationSendJob> _logger;
    private Timer _timer;
    
    
    public NotificationSendJob(ILogger<NotificationSendJob> logger)
    {
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
            // var notifications = _db.GetUnsendNotification();
            // if (notifications == null)
            // {
            //     _logger.LogError("[Notifications list in null]");
            // }
            
            Console.WriteLine("OK");
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