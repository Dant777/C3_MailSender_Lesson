using System;
using MailSender_Lib.Data;

namespace MailSender_Lib.Services.InMemory
{
    public class ServerDataInMemory : DataInMemory<Server>, IServerDataService
    {
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