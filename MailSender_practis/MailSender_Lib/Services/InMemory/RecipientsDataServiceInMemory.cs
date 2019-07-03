using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender_Lib.Data;

namespace MailSender_Lib.Services.InMemory
{
    public class RecipientsDataServiceInMemory : DataInMemory<Recipient>,IRecipientsDataService
    {
        public RecipientsDataServiceInMemory()
        {
            var test_data = new List<Recipient>
            {
                new Recipient {Id = 1, Name = "Name 1", Address = "addres_1@mail.ru", Description = "!!!!"},
                new Recipient {Id = 2, Name = "Name 2", Address = "addres_2@mail.ru", Description = "!!!!"},
                new Recipient {Id = 3, Name = "Name 3", Address = "addres_3@mail.ru", Description = "!!!!"}
            };
            _Items.AddRange(test_data);
        } 

       

        public void Edit(Recipient item)
        {
            if(item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(item.Id);
            if (db_item is null) return;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Description = item.Description;

        }

      
    }
}
