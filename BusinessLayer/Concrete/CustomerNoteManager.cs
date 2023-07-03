using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CustomerNoteManager : ICustomerNoteService
    {

        ICustomerNoteDal _customerNoteDal;

        public CustomerNoteManager(ICustomerNoteDal customerNoteDal)
        {
            _customerNoteDal = customerNoteDal;
        }

        public void CustomerNoteAdd(CustomerNote customerNote)
        {
            _customerNoteDal.Insert(customerNote);
        }

        public void CustomerNoteDelete(CustomerNote customerNote)
        {
            _customerNoteDal.Delete(customerNote);
        }

        public void CustomerNoteUpdate(CustomerNote customerNote)
        {
            _customerNoteDal.Update(customerNote);
        }

        public CustomerNote GetByID(int ID)
        {
            return _customerNoteDal.GetByID(ID);
        }

        public List<CustomerNote> GetList()
        {
            return _customerNoteDal.GetListAll();
        }
    }
}
