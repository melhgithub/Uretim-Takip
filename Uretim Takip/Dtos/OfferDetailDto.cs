using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uretim_Takip.Dtos
{
    public class OfferDetailDto
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int OfferID { get; set; }
        public decimal Price { get; set; }
        public double Piece { get; set; }
    }
}
