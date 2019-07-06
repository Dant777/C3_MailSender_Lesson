using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MailSender_Lib.Data;
using MailMessage = MailSender_Lib.Data.MailMessage;

namespace MailSender_Lib.Services
{
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

        public void SendParallel(MailMessage Message, Sender From, IEnumerable<Recipient> To)
        {
            throw new NotImplementedException();
        }

        public async Task SendAsync(MailMessage Message, Sender From, Recipient To)
        {
            using (var server = new SmtpClient(_host, _port) { EnableSsl = _ssl })
            {
                server.Credentials = new NetworkCredential(_login, _password);
                using (var msg = new System.Net.Mail.MailMessage())
                {
                    msg.From = new MailAddress(From.Address, From.Name);
                    msg.To.Add(new MailAddress(To.Address, To.Name));

                    await server.SendMailAsync(msg);
                }
            }
        }

        public async Task SendAsync(MailMessage Message, Sender From, IEnumerable<Recipient> To, IProgress<double> Progress = null,
            CancellationToken Cancel = default)
        {
            
            //await Task.WhenAll(To.Select(to => SendAsync(Message, From, to)));

            var to = To.ToArray();
            for (int i = 0; i < to.Length; i++)
            {
                Cancel.ThrowIfCancellationRequested();
                await SendAsync(Message, From, to[i]);
                Progress?.Report((double)1/to.Length);
            }
            Progress?.Report(1);
        }
    }
}