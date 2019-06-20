namespace MailSender_Lib.Data
{
    /// <summary>
    /// Класс письмо
    /// </summary>
    public class MailMessage
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
