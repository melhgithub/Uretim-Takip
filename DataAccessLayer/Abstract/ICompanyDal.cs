using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICompanyDal : IGenericDal<Company>
    {
        List<string> GetCompanyNames();
        Company GetByName(string companyName);
        Task<List<Company>> GetListAsync();
    }
}
