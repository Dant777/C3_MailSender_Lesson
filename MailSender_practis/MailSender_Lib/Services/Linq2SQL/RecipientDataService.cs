﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender_Lib.Data.Linq2SQL;

namespace MailSender_Lib.Services.Linq2SQL
{
    public class RecipientDataServiceLinq2SQL : IRecipientsDataService
    {
        public readonly MailSenderDBContext _db;
        public RecipientDataServiceLinq2SQL(MailSenderDBContext db)
        {
            _db = db;
        }

        public IEnumerable<Recipient> GetAll()
        {
            return _db.Recipient.ToArray();
        }
    }
}
