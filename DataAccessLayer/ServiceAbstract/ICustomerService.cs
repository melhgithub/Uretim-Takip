using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceAbstract
{
    public interface ICustomerService
    {
        void CustomerAdd(Customer customer);
        void CustomerDelete(Customer customer);
        void CustomerUpdate(Customer customer);
        List<Customer> GetList();
        Customer GetByID(int ID);
        List<string> GetCustomerNames();
        int GetCustomerIdByName(string customerName);
        Task<List<Customer>> GetListAsync();
    }
}
