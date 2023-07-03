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
    public class OfferDetailManager : IOfferDetailService
    {
        IOfferDetailDal _offerDetailDal;

        public OfferDetailManager(IOfferDetailDal offerDetailDal)
        {
            _offerDetailDal = offerDetailDal;
        }

        public OfferDetail GetByID(int ID)
        {
            return _offerDetailDal.GetByID(ID);
        }

        public List<OfferDetail> GetList()
        {
            return _offerDetailDal.GetListAll();
        }

        public void OfferDetailAdd(OfferDetail offerDetail)
        {
            _offerDetailDal.Insert(offerDetail);
        }

        public void OfferDetailDelete(OfferDetail offerDetail)
        {
            _offerDetailDal.Delete(offerDetail);
        }

        public void OfferDetailUpdate(OfferDetail offerDetail)
        {
            _offerDetailDal.Update(offerDetail);
        }
    }
}
