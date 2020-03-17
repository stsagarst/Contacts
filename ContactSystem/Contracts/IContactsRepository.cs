using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Contracts
{
   public interface IContactsRepository
    {
        IEnumerable<Contact> GetContact();
        Contact GetContactById(int id);
        void NewContact(Contact employee);
        void UpdateContact(Contact employee);
        void DeleteContact(int id);
      
    }
}
