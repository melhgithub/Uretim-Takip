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
    public class EFOfferRepository : GenericRepository<Offer>, IOfferDal
    {
        public List<Offer> GetListWithIncludes()
        {
            using (var context = new Context())
            {
                return context.Offers.Include(p => p.Customer)
                                       .Include(p => p.Company)
                                       .ToList();
            }
        }
        public async Task<List<Offer>> GetListWithIncludesAsync()
        {
            using (var context = new Context())
            {
                return await context.Offers
                    .Include(p => p.Customer)
                    .Include(p => p.Company).
                    ToListAsync();
            }
        }
    }
}
