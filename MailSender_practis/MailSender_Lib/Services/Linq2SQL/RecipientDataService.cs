using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MailSender_Lib.Data;

namespace MailSender_Lib.Services.Linq2SQL
{
    public class RecipientDataServiceLinq2SQL : IRecipientsDataService
    {
        public readonly MailSender_Lib.Data.Linq2SQL.MailSenderDBContext _db;
        public RecipientDataServiceLinq2SQL(MailSender_Lib.Data.Linq2SQL.MailSenderDBContext db)
        {
            _db = db;
        }

        public IEnumerable<Recipient> GetAll() => _db.Recipient
            .Select(r => new Recipient
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                Description = r.Description
            })
            .ToArray();
        

        public Recipient GetById(int id) => _db.Recipient
            .Select(r => new Recipient
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                Description = r.Description
            })
            .FirstOrDefault(r => r.Id == id);
       

        public void Add(Recipient item)
        {
            if (item.Id != 0) return;
            _db.Recipient.InsertOnSubmit(new Data.Linq2SQL.Recipient
            {
                Name = item.Name,
                Address = item.Address,
                Description = item.Description
            });
            _db.SubmitChanges();
        }

        public void Delete(Recipient item)
        {
            var db_item = _db.Recipient.FirstOrDefault(i => i.Id == item.Id);
            if(db_item is null) return;
            _db.Recipient.DeleteOnSubmit(db_item);
            _db.SubmitChanges();
        }

        public void Edit(Recipient item)
        {

            
            _db.SubmitChanges();
        }

    
    }
}
