using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFCustomerRepository : GenericRepository<Customer>, ICustomerDal
    {
        public List<string> GetCustomerNames()
        {
            using (var context = new Context())
            {
                return context.Customers.Select(c => c.Name).ToList();
            }
        }

        public Customer GetByName(string customerName)
        {
            using (var context = new Context())
            {
                return context.Customers.FirstOrDefault(c => c.Name == customerName);
            }
        }

        public async Task<List<Customer>> GetListAsync()
        {
            using (var context = new Context())
            {
                return await context.Customers.ToListAsync();
            }
        }
    }
}
