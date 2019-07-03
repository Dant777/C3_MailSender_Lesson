using MailSender_Lib.Data.BaseEntityes;

namespace MailSender_Lib.Data
{
    /// <summary>
    /// Класс письмо
    /// </summary>
    public class MailMessage : Entity
    {
        /// <summary>
        /// Тема письма 
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Сообщение письма
        /// </summary>
        public string Body { get; set; }

    }
}
