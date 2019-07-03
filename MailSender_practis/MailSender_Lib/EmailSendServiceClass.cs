using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using MailSender_Lib.Data.Linq2SQL;
using System.Collections.ObjectModel;

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
            try
            {
                message.From = new MailAddress(strLogin, "");
                message.To.Add(new MailAddress(mail, name));
                message.Subject = $"{strSubject}";
                message.Body = $"{strBody} \n Отправлено - {DateTime.Now}";

            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка при отправке почты \r\n{e.Message}", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);

            }

            try
            {
                using (var client = new SmtpClient(strSmtp, strPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(strLogin, strPassword);
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

        public void SendMails(ObservableCollection<Recipient> emails)
        {
            foreach (Recipient email in emails)
            {
                SendMail(email.Address, email.Name);
            }
        }
    }

}
