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

        [Description("Siparişte")]
        Approved = 1,

        [Description("KALDIRILDI")]
        Removed = 2,

        [Description("Sipariş İptal Edildi")]
        Cancelled = 3,

        [Description("Üretime Geçildi")]
        Finished = 4
    }
    public class Offer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public double Piece { get; set; }
        [Required]
        public OfferStatuses Status { get; set; }


        //BAĞLANTILAR

        public List<OfferDetail> OfferDetail { get; set; }

        
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public int CompanyID { get; set; }
        public Company Company { get; set; }


    }
}
