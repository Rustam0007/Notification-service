using System.ComponentModel.DataAnnotations;

namespace CallbackService.Models;

public class NotificationRequest
{
    public string? OrderType { get; set; }
    public string? SessionId { get; set; }
    public string? Card { get; set; }
    public string? EventDate { get; set; }
    public string? WebsiteUrl { get; set; }
    [Range(1, 1000)]
    [Required]
    public int CardId { get; set; }
}

public class NotificationResponse
{
    public int Id { get; set; }

}