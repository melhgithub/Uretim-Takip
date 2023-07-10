using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uretim_Takip.Dtos
{
    public class ProductionFilterDto
    {
        public decimal Price { get; set; }
        public int ProductPiece { get; set; }
        public ProductionStatuses Status { get; set; }

        public List<string> Customers { get; set; }
        public string CustomerName { get; set; }

        public List<string> Companies { get; set; }
        public string CompanyName { get; set; }
    }
}
