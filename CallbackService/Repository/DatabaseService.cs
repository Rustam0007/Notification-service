using System.Collections;
using CallbackService.Models;
using Dapper;
using Newtonsoft.Json;
using Npgsql;

namespace CallbackService.Repository;

public class DatabaseService : IDatabaseService
{
    private static string? _connString;
    public DatabaseService(IConfiguration configuration)
    {
        _connString = configuration["DBConnectionString"];
    }
    private NpgsqlConnection GetNgpsqlConnection()
    {
        var conn = new NpgsqlConnection(_connString);
        conn.Open();
        return conn;
    }
    
    public long InsertNotification(int cardId, string message)
    {
        var conn = GetNgpsqlConnection();
    
        
        var sql = $"INSERT INTO notification (card_id, message, is_send) VALUES (@CardId, @Message, @IsSend) RETURNING id";
        var notificationId = (long)conn.ExecuteScalar(sql, new { cardId, message, IsSend = false });
        
        return notificationId;
    }

    public IEnumerable <NotificationJoin> GetUnsendNotification()
    {
        var conn = GetNgpsqlConnection();
        
        var sql = $"SELECT n.id, n.message, c.phone FROM notification n JOIN card c ON n.card_id = c.id WHERE not n.is_Send";
        return conn.Query<NotificationJoin>(sql);
        
    }

    public void UpdateNotificationStatus(int notificationId)
    {
        var conn = GetNgpsqlConnection();

        var sql = $"UPDATE notification SET is_send = @IsSend WHERE id = @Id";
        var parameters = new { IsSend = true, Id = notificationId };
        conn.Execute(sql, parameters);
    }
}