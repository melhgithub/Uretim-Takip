using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceAbstract
{
    public interface IOrderService
    {
        void OrderAdd(Order order);
        void OrderDelete(Order order);
        void OrderDeleteAdmin(Order order);
        void OrderUpdate(Order order);
        List<Order> GetList();
        Order GetByID(int ID);
        Task<List<Order>> GetListAsync();
    }
}
