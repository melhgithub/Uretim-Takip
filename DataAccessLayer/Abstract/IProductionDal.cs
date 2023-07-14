using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IProductionDal : IGenericDal<Production>
    {
        Task<List<Production>> GetListAsync();
    }
}
