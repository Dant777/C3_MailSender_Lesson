using MailSender_Lib.Data;

namespace MailSender_Lib.Services
{
    public class SmtpMailSenderServicr : IMailSenderService
    {
        public IMailSender CreateSender(Server server) => new SmtpMailSender(server.Address, server.Port, server.UseSSl,server.UserName, server.Password);
    }
}