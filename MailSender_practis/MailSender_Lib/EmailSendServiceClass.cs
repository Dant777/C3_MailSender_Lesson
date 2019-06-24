using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace MailSender_Lib
{

    class EmailSendServiceClass
    {
        MailMessage message = new MailMessage();
        //Sender
        private string SenderAddress = String.Empty;
        private string SenderName = String.Empty;
        //Recipient
        private string RecipientAddress = String.Empty;
        private string RecipientName = String.Empty;

        /// <summary>
        /// Конструктор класса, подаюстся адреса и имена отправителя и получателя 
        /// </summary>
        /// <param name="senderName">Имя отправителя</param>
        /// <param name="senderAddress">Адрес отправителя</param>
        /// <param name="recipientName">Имя получателя</param>
        /// <param name="recipientAddress">Адрес получателя</param>
        public EmailSendServiceClass(string senderName, string senderAddress, string recipientName, string recipientAddress)
        {
            this.SenderName = senderName;
            this.SenderAddress = senderAddress;

            this.RecipientName = recipientName;
            this.RecipientAddress = recipientAddress;
        }
        /// <summary>
        /// Создание письма
        /// </summary>
        public void CreateMailMessage(string subject, string body)
        {
            try
            {
                message.From = new MailAddress(SenderAddress, SenderName);
                message.To.Add(new MailAddress(RecipientAddress, RecipientName));
                message.Subject = $"{subject}";
                message.Body = $"{body} \n Отправлено - {DateTime.Now}";

            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка при отправке почты \r\n{e.Message}", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

        /// <summary>
        /// Отправка письма
        /// </summary>
        /// <param name="cliendAddress">Адрес сервера</param>
        /// <param name="clientPort">Порт сервера</param>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <param name="flag">Использование Secure Sokets</param>
        public void SendMail(string cliendAddress, int clientPort, string userName, string password, bool flag)
        {
            try
            {
                using (var client = new SmtpClient(cliendAddress, clientPort))
                {
                    client.EnableSsl = flag;
                    client.Credentials = new NetworkCredential(userName, password);
                    client.Send(message);

                    MessageBox.Show("Почта отправлена успешно!", "Успех!!!",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка при отправке почты \r\n{e.Message}", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }

}
