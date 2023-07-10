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
    public enum OfferStatuses
    {
        // Status.Removed.DisplayName(); //KALDIRILDI ŞEKLİNDE KULLANILACAK
        [Description("TÜMÜ")]
        All = 0,

        [Description("Teklifte")]
        Approved = 1,

        [Description("KALDIRILDI")]
        Removed = 2,

        [Description("Teklif İptal Edildi")]
        Cancelled = 3,

        [Description("Siparişe Geçildi")]
        Finished = 4,
            
        [Description("Beklemede")]
        Pending = 5,
    }
    public class Offer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public OfferStatuses Status { get; set; }

        [Required]
        public int ProductPiece { get; set; }


        //BAĞLANTILAR

        public List<OfferDetail> OfferDetail { get; set; }


        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public int CompanyID { get; set; }
        public Company Company { get; set; }


    }
}
