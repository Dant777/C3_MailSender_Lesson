using System;
using MailSender_Lib.Data;

namespace MailSender_Lib.Services.InMemory
{
    public class MailMessagesDataInMemory : DataInMemory<MailMessage>, IMailMessageDataService
    {
        public MailMessagesDataInMemory()
        {
            for (int i = 1; i < 10; i++)
            {
                _Items.Add(new MailMessage{Subject = $"Письмо {i}", Body = $"Text message{i}"});
            }
        }
        public override void Edit(MailMessage item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            var db_item = GetById(item.Id);
            if (db_item is null) return;

            db_item.Subject = item.Subject;
            db_item.Body = item.Body;
        }

    }
}