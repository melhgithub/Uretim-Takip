using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uretim_Takip.Dtos
{
    public class CategoryEditDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public CategoryStatuses Status { get; set; }
    }
}
