using DataAccessLayer.Abstract;
using DataAccessLayer.ServiceAbstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceConcrete
{
    public class OrderManager : IOrderService
    {

        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public Order GetByID(int ID)
        {
            return _orderDal.GetByID(ID);
        }

        public List<Order> GetList()
        {
            return _orderDal.GetListAll();
        }

        public void OrderAdd(Order order)
        {
            _orderDal.Insert(order);
        }

        public void OrderDelete(Order order)
        {
            order.Status = (OrderStatuses)3;
            _orderDal.Update(order);
        }

        public void OrderDeleteAdmin(Order order)
        {
            order.Status = (OrderStatuses)2;
            _orderDal.Update(order);
        }

        public void OrderUpdate(Order order)
        {
            _orderDal.Update(order);
        }
    }
}
