using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        void SendParallel(MailMessage Message, Sender From, IEnumerable<Recipient> To);
        Task SendAsync(MailMessage Message, Sender From, Recipient To);
        Task SendAsync(MailMessage Message, Sender From, IEnumerable<Recipient> To, IProgress<double> Progress = null, CancellationToken Cancel = default);
    }
}
