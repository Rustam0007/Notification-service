using System.Collections;
using CallbackService.Models;

namespace CallbackService.Repository;

public interface IDatabaseService
{
    public int InsertNotification(int cardId, string message);
    public IEnumerable <NotificationJoin> GetUnsendNotification();
    public void UpdateNotificationStatus(int notificationId);
}