namespace MailSender_Lib.Data
{
    /// <summary>
    /// Класс сервера от которого идет письмо
    /// </summary>
    class Server
    {
        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Порт сервера
        /// </summary>
        public int Port { get; set; } = 25;
        /// <summary>
        /// Формат передачи данных с шифрованием
        /// </summary>
        public bool UseSSl { get; set; } = true;
        /// <summary>
        /// Логин на сервере
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
