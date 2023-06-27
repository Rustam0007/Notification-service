using CallbackService.Models;
using Npgsql;
using Dapper;
using Newtonsoft.Json;

namespace CallbackService.Services;
public class NotificationService
{
    private static string? _connString;
    public NotificationService(IConfiguration configuration)
    {
        _connString = configuration["DBConnectionString"];
    }
    private NpgsqlConnection GetNgpsqlConnection()
    {
        var conn = new NpgsqlConnection(_connString);
        conn.Open();
        return conn;
    }
    public NotificationRes Create(NotificationReq notificationReq)
    {
        var conn = GetNgpsqlConnection();
        var orderJson = JsonConvert.SerializeObject(notificationReq);

        
        var sql = $"INSERT INTO notification (\"cardId\", message, issend) VALUES (@CardId, @Message, @IsSend) RETURNING *";
        var parameters = new {notificationReq.CardId, Message = orderJson, IsSend = false};
        
        var notification = conn.QueryFirst<NotificationRes>(sql, parameters);
        
        return notification;
    }
}