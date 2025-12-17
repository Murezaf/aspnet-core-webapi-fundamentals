namespace Smaple01.Services.MailService
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}
