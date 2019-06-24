using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender_Lib.Data
{
    public static class SmtpServer
    {
        public static Dictionary<string, string> Servers
        {
            get { return dicServers; }
        }
        private static Dictionary<string, string> dicServers = new Dictionary<string, string>()
        {
            { "smtp.yandex.ru","25" },
            
        };

    }
}
