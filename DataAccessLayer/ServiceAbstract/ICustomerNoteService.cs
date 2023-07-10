using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceAbstract
{
    public interface ICustomerNoteService
    {
        void CustomerNoteAdd(CustomerNote customerNote);
        void CustomerNoteDelete(CustomerNote customerNote);
        void CustomerNoteUpdate(CustomerNote customerNote);
        List<CustomerNote> GetList();
        CustomerNote GetByID(int ID);
    }
}
