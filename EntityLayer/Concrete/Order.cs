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
    public enum OrderStatuses
    {
        // Status.Removed.DisplayName(); //KALDIRILDI ŞEKLİNDE KULLANILACAK
        [Description("TÜMÜ")]
        All = 0,

        [Description("Siparişte")]
        Approved = 1,

        [Description("KALDIRILDI")]
        Removed = 2,

        [Description("Sipariş İptal Edildi")]
        Cancelled = 3,

        [Description("Üretime Geçildi")]
        Finished = 4
    }
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        public int ProductPiece { get; set; }
        [Required]
        public OrderStatuses Status { get; set; }

        //BAĞLANTILAR

        public int OfferID { get; set; }
        public Offer Offer { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public int CompanyID { get; set; }
        public Company Company { get; set; }

    }
}
