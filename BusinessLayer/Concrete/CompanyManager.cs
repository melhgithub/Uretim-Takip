using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
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

        public List<Company> GetList()
        {
            return _companyDal.GetListAll();
        }
    }
}
