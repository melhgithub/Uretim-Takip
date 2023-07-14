using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.ServiceAbstract;
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
    public class EFProductRepository : GenericRepository<Product>, IProductDal
    {
        public async Task<List<Product>> GetListByCompanyID(int companyID)
        {
            using (var context = new Context())
            {
                return await context.Products
                .Where(p => p.CompanyID == companyID)
                .ToListAsync();
            }
        }

        public List<Product> GetListWithIncludes()
        {
            using (var context = new Context())
            {
                return context.Products.Include(p => p.Category)
                                       .Include(p => p.Company)
                                       .ToList();
            }
        }

        public async Task<List<Product>> GetListWithIncludesAsync()
        {
            using (var context = new Context())
            {
                return await context.Products.Include(p => p.Category)
                                             .Include(p => p.Company)
                                             .ToListAsync();
            }
        }

    }
}
