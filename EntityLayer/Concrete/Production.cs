using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public enum ProductionStatuses
    {
        // Status.Removed.DisplayName(); //KALDIRILDI ŞEKLİNDE KULLANILACAK
        [Description("Üretimde")]
        Approved = 1,

        [Description("KALDIRILDI")]
        Removed = 2,

        [Description("Üretim İptal Edildi")]
        Cancelled = 3,


        [Description("Üretim Tamamlandı")]
        Finished = 4
    }

    public class Production
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Piece { get; set; }
        [Required]
        public ProductionStatuses Status { get; set; }


        //BAĞLANTILAR

        public int OfferID { get; set; }
        public Offer Offer { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public int CompanyID { get; set; }
        public Company Company { get; set; }

        

    }
}
