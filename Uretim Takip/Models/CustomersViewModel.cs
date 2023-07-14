using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uretim_Takip.Dtos;

namespace Uretim_Takip.Models
{
    public class CustomersViewModel
    {
        public CustomerFilterDto FilterDto { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
