using System.Diagnostics;
using Dapper;
using Newtonsoft.Json;
using Npgsql;

namespace CallbackService.JobManager;

public class NotificationSendJob : IHostedService
{
    private static string? _connString;
    private readonly ILogger<NotificationSendJob> _logger;
    private Timer _timer;
    
    public NotificationSendJob(ILogger<NotificationSendJob> logger, IConfiguration configuration, Timer timer)
    {
        _logger = logger;
        _timer = timer;
        _connString = configuration["DBConnectionString"];
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
            using (var connection = new NpgsqlConnection(_connString))
            {
                var sql = "SELECT message, \"cardId\" FROM notification WHERE isSend = false";
                var notifications = connection.Query(sql);

                foreach (var notification in notifications)
                {
                    var message = JsonConvert.DeserializeObject(notification.message);
                    int cardId = notification.cardId;


                    var cardSql = "SELECT phone FROM card WHERE id = @CardId";
                    var cardPhone = connection.QueryFirstOrDefault<string>(cardSql, new {CardId = cardId});

                    SmsClient.SendByPhoneNumber(cardPhone, message.EventDate, message.OrderType, message.Card,
                        message.WebsiteUrl);
                    
                    var updateSql = $"UPDATE notification SET isSend = @IsSend WHERE \"cardId\" = @CardId RETURNING *";
                    var parameters = new { IsSend = true, CardId = cardId };
                    connection.QueryFirst(updateSql, parameters);
                    
                    _timer.Change(2000, Timeout.Infinite);

                }
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