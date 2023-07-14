using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceAbstract
{
    public interface IProductionService
    {
        void ProductionAdd(Production production);
        void ProductionDelete(Production production);
        void ProductionDeleteAdmin(Production production);
        void ProductionUpdate(Production production);
        List<Production> GetList();
        Production GetByID(int ID);
        Task<List<Production>> GetListAsync();
    }
}
