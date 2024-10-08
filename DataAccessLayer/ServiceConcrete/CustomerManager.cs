﻿using DataAccessLayer.Abstract;
using DataAccessLayer.ServiceAbstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceConcrete
{
    public class CustomerManager : ICustomerService
    {

        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public void CustomerAdd(Customer customer)
        {
            _customerDal.Insert(customer);
        }

        public void CustomerDelete(Customer customer)
        {
            _customerDal.Delete(customer);
        }

        public void CustomerUpdate(Customer customer)
        {
            _customerDal.Update(customer);
        }

        public Customer GetByID(int ID)
        {
            return _customerDal.GetByID(ID);
        }

        public List<Customer> GetList()
        {
            return _customerDal.GetListAll();
        }
        public async Task<List<Customer>> GetListAsync()
        {
            return await _customerDal.GetListAsync();
        }

        public List<string> GetCustomerNames()
        {
            return _customerDal.GetListAll().Select(c => c.Name).ToList();
        }

        public int GetCustomerIdByName(string customerName)
        {
            var customer = _customerDal.GetByName(customerName);
            return customer != null ? customer.ID : 0;
        }
    }
}
