using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender_Lib.Data
{
    class SmtpServer
    {
        public static Dictionary<string, string> Server
        {
            get { return dicServer; }
        }
        private static Dictionary<string, string> dicServer = new Dictionary<string, string>()
        {
            { "smtp.yandex.ru","25" },
            
        };

    }
}
