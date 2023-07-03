using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Uretim_Takip.Dtos
{
    public class ProductFilterDto
    {
        public string Name { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public decimal Piece { get; set; }
        public ProductStatuses Status { get; set; }
        public string Description { get; set; }

        public List<string> Categories { get; set; }
        public string CategoryName { get; set; }

        public List<string> Companies { get; set; }
        public string CompanyName { get; set; }
    }
}