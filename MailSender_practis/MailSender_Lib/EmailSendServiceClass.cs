using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using MailSender_Lib.Data.Linq2SQL;

namespace MailSender_Lib
{
    
    public class EmailSendServiceClass
    {
        MailMessage message = new MailMessage();

        //User
        private string strLogin;         // email, c которого будет рассылаться почта
        private string strPassword;  // пароль к email, с которого будет рассылаться почта
        private string strSmtp;
        private int strPort;
        private string strBody;                    // текст письма для отправки
        private string strSubject;                 // тема письма для отправки



        /// <summary>
        /// Конструктор класса дла ввхода на сервер 
        /// </summary>
        /// <param name="userLogin">Логин пользователя</param>
        /// <param name="userPassword">Пароль пользователя</param>
        /// <param name="userSmtp">Сервер</param>
        /// <param name="clientPort">Порт</param>
        /// <param name="strBody">Тело письма</param>
        /// <param name="strSubject">Тема письма</param>
        public EmailSendServiceClass(string userLogin, string userPassword, string userSmtp, int clientPort, string strBody, string strSubject)
        {
            strLogin = userLogin;
            strPassword = userPassword;
            strSmtp = userSmtp;
            strPort = clientPort;
            this.strBody = strBody;
            this.strSubject = strSubject;
        }
        /// <summary>
        /// Создание письма
        /// </summary>
 
        private void SendMail(string mail, string name)
        {
            
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = strBody;
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp, strPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, strPassword);
                try
                {
                    sc.Send(mm);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Невозможно отправить письмо " + e.ToString());
                }

            }
        }

        public void SendMails(IQueryable<Recipient> emails)
        {
            foreach (Recipient email in emails)
            {
                SendMail(email.Address, email.Name);
            }
        }
    }

}
