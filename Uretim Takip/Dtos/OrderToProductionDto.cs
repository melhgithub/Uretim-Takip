using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uretim_Takip.Dtos
{
    public class OrderToProductionDto
    {

        public int ID { get; set; }
        public OrderStatuses Status { get; set; }
    }
}
