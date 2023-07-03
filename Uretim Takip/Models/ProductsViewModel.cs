using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uretim_Takip.Dtos;

namespace Uretim_Takip.Models
{
    public class ProductsViewModel
    {
        public ProductFilterDto FilterDto { get; set; }
        public List<Product> Products { get; set; }
    }
}
