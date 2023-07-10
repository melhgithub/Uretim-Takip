using DataAccessLayer.Abstract;
using DataAccessLayer.ServiceAbstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceConcrete
{
    public class CompanyManager : ICompanyService
    {

        ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public void CompanyAdd(Company company)
        {
            _companyDal.Insert(company);
        }

        public void CompanyDelete(Company company)
        {
            _companyDal.Delete(company);
        }

        public void CompanyUpdate(Company company)
        {
            _companyDal.Update(company);
        }

        public Company GetByID(int ID)
        {
            return _companyDal.GetByID(ID);
        }

        public int GetCompanyIdByName(string companyName)
        {
            var company = _companyDal.GetByName(companyName);
            return company != null ? company.ID : 0;
        }

        public List<string> GetCompanyNames()
        {
            return _companyDal.GetListAll().Select(c => c.Name).ToList();
        }

        public List<Company> GetList()
        {
            return _companyDal.GetListAll();
        }
        public async Task<List<Company>> GetListAsync()
        {
            return await _companyDal.GetListAsync();
        }
    }
}
