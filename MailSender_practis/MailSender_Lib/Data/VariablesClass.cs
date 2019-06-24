using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender_Lib.Data
{
    /// <summary>
    /// Содержит коллекция отправителей с адресами и паролями
    /// </summary>
    public static class VariablesClass
    {
        public static Dictionary<string, string> Senders
        {
            get { return dicSenders; }
        }
        private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        {
            { "??????@yandex.ru","" },
            { "sok74@yandex.ru",PasswordClass.getPassword(";liq34tjk") }
        };
    }
}
