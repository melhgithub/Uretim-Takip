using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uretim_Takip.Dtos
{
    public class ProductionDeleteDto
    {

        public int ID { get; set; }
        public ProductionStatuses Status { get; set; }
    }
}
