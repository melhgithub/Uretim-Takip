using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICompanyService
    {
        void CompanyAdd(Company company);
        void CompanyDelete(Company company);
        void CompanyUpdate(Company company);
        List<Company> GetList();
        Company GetByID(int ID);
    }
}
