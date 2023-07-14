using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceAbstract
{
    public interface IOfferService
    {
        void OfferAdd(Offer offer);
        void OfferDelete(Offer offer);
        void OfferDeleteAdmin(Offer offer);
        void OfferUpdate(Offer offer);
        List<Offer> GetList();
        Offer GetByID(int ID);
        List<Offer> GetListWithIncludes();
        Task<List<Offer>> GetListWithIncludesAsync();
    }
}
