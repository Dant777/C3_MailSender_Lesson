using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender_Lib.Services
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Edit(T item);
        void Add(T item);
        void Delete(T item);
    }
}
