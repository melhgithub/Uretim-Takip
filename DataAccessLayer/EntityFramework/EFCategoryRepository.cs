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
    public class EFCategoryRepository : GenericRepository<Category>, ICategoryDal
    {
        public List<string> GetCategoryNames()
        {
            using (var context = new Context())
            {
                return context.Categories.Select(c => c.Name).ToList();
            }
        }

        public Category GetByName(string categoryName)
        {
            using (var context = new Context())
            {
                return context.Categories.FirstOrDefault(c => c.Name == categoryName);
            }
        }

        public async Task<List<Category>> GetListAsync()
        {
            using (var context = new Context())
            {
                return await context.Categories.ToListAsync();
            }
        }
    }
}
