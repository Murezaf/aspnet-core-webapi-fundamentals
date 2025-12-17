namespace Smaple01.Services.MailServices
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}
