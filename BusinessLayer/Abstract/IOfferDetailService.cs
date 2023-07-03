using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOfferDetailService
    {
        void OfferDetailAdd(OfferDetail offerDetail);
        void OfferDetailDelete(OfferDetail offerDetail);
        void OfferDetailUpdate(OfferDetail offerDetail);
        List<OfferDetail> GetList();
        OfferDetail GetByID(int ID);
    }
}
