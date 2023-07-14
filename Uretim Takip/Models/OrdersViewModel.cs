using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uretim_Takip.Dtos;

namespace Uretim_Takip.Models
{
    public class OrdersViewModel
    {
        public OrderFilterDto FilterDto { get; set; }
        public List<Order> Orders { get; set; }
    }
}
