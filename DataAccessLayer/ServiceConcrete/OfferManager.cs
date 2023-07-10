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
    public class OfferManager : IOfferService
    {
        IOfferDal _offerDal;

        public OfferManager(IOfferDal offerDal)
        {
            _offerDal = offerDal;
        }

        public Offer GetByID(int ID)
        {
            return _offerDal.GetByID(ID);
        }

        public List<Offer> GetList()
        {
            return _offerDal.GetListAll();
        }

        public void OfferAdd(Offer offer)
        {
            _offerDal.Insert(offer);
        }

        public void OfferDelete(Offer offer)
        {
            offer.Status = (OfferStatuses)3;
            _offerDal.Update(offer);
        }

        public void OfferDeleteAdmin(Offer offer)
        {
            offer.Status = (OfferStatuses)2;
            _offerDal.Update(offer);
        }

        public void OfferUpdate(Offer offer)
        {
            _offerDal.Update(offer);
        }
    }
}
