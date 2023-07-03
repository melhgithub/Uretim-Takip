using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class OfferDetail
    {
        [Key]
        public int ID { get; set; }

        public decimal Price { get; set; }
        public double Piece { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }


        public int OfferID { get; set; }
        public Offer Offer { get; set; }


    }
}
