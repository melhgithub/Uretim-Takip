using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uretim_Takip.Dtos
{
    public class OfferEditDto
    {
        public int ID { get; set; }
        public int ProductPiece { get; set; }

        private string _price;

        public string Price
        {
            get { return _price; }
            set { _price = value.Replace(",", "."); }
        }
        public OfferStatuses Status { get; set; }

        public string CustomerName { get; set; }

        public string CompanyName { get; set; }
    }
}
