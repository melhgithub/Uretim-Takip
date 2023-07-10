using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uretim_Takip.Dtos
{
    public class OfferToOrderDto
    {

        public int ID { get; set; }
        public OfferStatuses Status { get; set; }
    }
}
