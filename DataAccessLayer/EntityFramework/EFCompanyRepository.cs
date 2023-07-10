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
    public class EFCompanyRepository : GenericRepository<Company>, ICompanyDal
    {
        public Company GetByName(string companyName)
        {
            using (var context = new Context())
            {
                return context.Companies.FirstOrDefault(c => c.Name == companyName);
            }
        }

        public List<string> GetCompanyNames()
        {
            using (var context = new Context())
            {
                return context.Companies.Select(c => c.Name).ToList();
            }
        }

        public async Task<List<Company>> GetListAsync()
        {
            using (var context = new Context())
            {
                return await context.Companies.ToListAsync();
            }
        }
    }
}
