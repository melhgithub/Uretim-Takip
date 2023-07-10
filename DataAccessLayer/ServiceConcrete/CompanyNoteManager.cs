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
    public class CompanyNoteManager : ICompanyNoteService
    {
        ICompanyNoteDal _companyNoteDal;

        public CompanyNoteManager(ICompanyNoteDal companyNoteDal)
        {
            _companyNoteDal = companyNoteDal;
        }

        public void CompanyNoteAdd(CompanyNote companyNote)
        {
            _companyNoteDal.Insert(companyNote);
        }

        public void CompanyNoteDelete(CompanyNote companyNote)
        {
            _companyNoteDal.Delete(companyNote);
        }

        public void CompanyNoteUpdate(CompanyNote companyNote)
        {
            _companyNoteDal.Update(companyNote);
        }

        public CompanyNote GetByID(int ID)
        {
            return _companyNoteDal.GetByID(ID);
        }

        public List<CompanyNote> GetList()
        {
            return _companyNoteDal.GetListAll();
        }
    }
}
