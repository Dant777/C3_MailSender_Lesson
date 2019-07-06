using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender_Lib.Data;

namespace MailSender_Lib.Services.InMemory
{
    public class SendersDataInMemory : DataInMemory<Sender>, ISenderDataService
    {
        public SendersDataInMemory()
        {
            for (int i = 1; i < 10; i++)
            {
                _Items.Add(new Sender() {Id = 1, Name = $"Sender Name{i}", Address = $"sender{i}@mail.com" });
            }
        }
        public override void Edit(Sender item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            var db_item = GetById(item.Id);
            if (db_item is null) return;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
        }
    }
}
