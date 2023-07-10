using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceAbstract
{
    public interface ICompanyNoteService
    {
        void CompanyNoteAdd(CompanyNote companyNote);
        void CompanyNoteDelete(CompanyNote companyNote);
        void CompanyNoteUpdate(CompanyNote companyNote);
        List<CompanyNote> GetList();
        CompanyNote GetByID(int ID);
    }
}
