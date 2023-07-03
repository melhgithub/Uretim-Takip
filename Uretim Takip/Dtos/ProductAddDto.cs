using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uretim_Takip.Dtos
{
    public class ProductAddDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        private string _price;

        public string Price
        {
            get { return _price; }
            set { _price = value.Replace(",", "."); }
        }

        private string _piece;

        public string Piece
        {
            get { return _piece; }
            set { _piece = value.Replace(",", "."); }
        }

        public ProductStatuses Status { get; set; }
        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string CompanyName { get; set; }
    }
}