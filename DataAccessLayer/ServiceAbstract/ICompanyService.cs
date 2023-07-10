using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceAbstract
{
    public interface ICompanyService
    {
        void CompanyAdd(Company company);
        void CompanyDelete(Company company);
        void CompanyUpdate(Company company);
        List<Company> GetList();
        List<string> GetCompanyNames();
        Company GetByID(int ID);
        int GetCompanyIdByName(string companyName);
        Task<List<Category>> GetListAsync();
    }
}
