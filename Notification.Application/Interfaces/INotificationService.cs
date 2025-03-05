namespace Notification.Application.Interfaces
{
    public interface INotificationService<in T>
    {
        Task SendNotification(T message);
    }
}
