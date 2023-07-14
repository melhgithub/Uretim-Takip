using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uretim_Takip.Dtos;

namespace Uretim_Takip.Models
{
    public class EditOfferViewModel
    {
        public Offer Offer { get; set; }
        public OfferFilterDto FilterDto { get; set; }
    }
}
