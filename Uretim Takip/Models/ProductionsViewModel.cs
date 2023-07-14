using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uretim_Takip.Dtos;

namespace Uretim_Takip.Models
{
    public class ProductionsViewModel
    {
        public ProductionFilterDto FilterDto { get; set; }
        public List<Production> Productions { get; set; }
    }
}
