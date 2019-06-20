using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender_Lib.Data
{
    public static class TestData
    {
        public static Server[] Servers { get; } = Enumerable.Range(1, 10)
            .Select(i => new Server
            {
                Name = $"Server {i}",
                Address = $"smtp.server{i}.ru",
                Port = 25,
                UserName = $"Server {i} user",
                Password = $"password {i}"
                
            }).ToArray();
        public static Sender[] Senders { get; } = Enumerable.Range(1, 10)
          .Select(i => new Sender
          {
              Name = $"Name {i}",
              Address = $"sender{i}@sender.ru",


          }).ToArray();
        public static MailMessage[] Messages { get; } = Enumerable.Range(1, 10)
        .Select(i => new MailMessage
        {
            Subject = $"Message {i}",
            Body = $"Text {i}",


        }).ToArray();
    }

}
