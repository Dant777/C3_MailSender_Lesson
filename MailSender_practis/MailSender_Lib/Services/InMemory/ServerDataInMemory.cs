using System;
using MailSender_Lib.Data;

namespace MailSender_Lib.Services.InMemory
{
    public class ServerDataInMemory : DataInMemory<Server>, IServerDataService
    {
        public ServerDataInMemory()
        {
            for (int i = 1; i < 10; i++)
            {
                _Items.Add(new Server() { Id = 1, Name = $"Sender Name{i}", Address = $"smtp.server{i}.com", Port = 25, Password = $"password{i}"});
            }
        }
        public override void Edit(Server item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            var db_item = GetById(item.Id);
            if (db_item is null) return;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Port = item.Port;
            db_item.UserName = item.UserName;
            db_item.Password = item.Password;
        }

    }
}