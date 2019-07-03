using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailSender_Lib.Data;
using MailMessage = MailSender_Lib.Data.MailMessage;

namespace MailSender_Lib.Services
{
    public interface IMailSenderService
    {
        IMailSender CreateSender(Server Server);
    }

    public interface IMailSender
    {
        void Send(MailMessage Message, Sender From, Recipient To);
        void Send(MailMessage Message, Sender From, IEnumerable<Recipient> To);
    }

    public class SmtpMailSenderServicr : IMailSenderService
    {
        public IMailSender CreateSender(Server server) => new SmtpMailSender(server.Address, server.Port, server.UseSSl,server.UserName, server.Password);
    }

    public class SmtpMailSender : IMailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _ssl;
        private readonly string _login;
        private readonly string _password;

        public SmtpMailSender(string Host, int Port, bool SSL, string Login, string Password)
        {
            _host = Host;
            _port = Port;
            _ssl = SSL;
            _login = Login;
            _password = Password;
        }

        public void Send(MailMessage Message, Sender From, Recipient To)
        {
            using (var server = new SmtpClient(_host, _port) {EnableSsl = _ssl})
            {
                server.Credentials = new NetworkCredential(_login, _password);
                using (var msg = new System.Net.Mail.MailMessage())
                {
                    msg.From = new MailAddress(From.Address,From.Name);
                    msg.To.Add(new MailAddress(To.Address,To.Name));

                    server.Send(msg);
                }
            }
        }

        public void Send(MailMessage Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To) Send(Message, From, recipient);
        }
    }
}
