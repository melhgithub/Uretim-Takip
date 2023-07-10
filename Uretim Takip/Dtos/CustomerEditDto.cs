using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uretim_Takip.Dtos
{
    public class CustomerEditDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public CustomerStatuses Status { get; set; }
    }
}
