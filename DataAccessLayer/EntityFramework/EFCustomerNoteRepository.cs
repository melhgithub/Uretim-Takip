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
    public class EFCustomerNoteRepository : GenericRepository<CustomerNote>, ICustomerNoteDal
    {
        public async Task<List<CustomerNote>> GetListAsync()
        {
            using (var context = new Context())
            {
                return await context.CustomerNotes.ToListAsync();
            }
        }
    }
}
