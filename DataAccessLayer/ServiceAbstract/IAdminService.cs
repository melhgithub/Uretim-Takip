using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceAbstract
{
    public interface IAdminService
    {
        void AdminAdd(Admin admin);
        void AdminDelete(Admin admin);
        void AdminUpdate(Admin admin);
        List<Admin> GetList();
        Admin GetByID(int ID);
    }
}
