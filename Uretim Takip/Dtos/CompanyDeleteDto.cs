﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uretim_Takip.Dtos
{
    public class CompanyDeleteDto
    {
        public int ID { get; set; }
        public CompanyStatuses Status { get; set; }
    }
}
