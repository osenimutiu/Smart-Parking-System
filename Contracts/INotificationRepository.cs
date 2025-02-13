namespace SmartParkingSystem.Contracts
{
    public interface INotificationRepository
    {
        bool SendEmail(string toEmail, string Location, DateTime StartDate, DateTime EndDate, bool IsBodyHtml = false);
    }
}
