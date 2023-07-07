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
    
    public int InsertNotification(int cardId, string message)  
    {
        using var conn = GetNgpsqlConnection();

        var sql = $"INSERT INTO notification (card_id, message, is_send) VALUES (@CardId, @Message, @IsSend) RETURNING id";
        return (int)conn.ExecuteScalar(sql, new { CardId = cardId, Message = message, IsSend = false });
    }

    public IEnumerable <NotificationJoin> GetUnsendNotification()
    {
        using var conn = GetNgpsqlConnection();
        
        var sql = $"SELECT n.id, n.message, c.phone FROM notification n INNER JOIN card c on c.id = n.card_id WHERE not n.is_send";
        return conn.Query<NotificationJoin>(sql);
    }

    public void UpdateNotificationStatus(int notificationId)
    {
        using var conn = GetNgpsqlConnection();

        var sql = $"UPDATE notification SET is_send = @IsSend WHERE id = @Id";
        var parameters = new { IsSend = true, Id = notificationId };
        conn.Execute(sql, parameters);
    }
}