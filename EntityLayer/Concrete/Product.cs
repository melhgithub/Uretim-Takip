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
    public enum ProductStatuses
    {
        // Status.Removed.DisplayName(); //KALDIRILDI ŞEKLİNDE KULLANILACAK

        [Description("SATIŞTA")]
        Approved = 1,

        [Description("KALDIRILDI")]
        Removed = 2,

        [Description("ONAY BEKLİYOR")]
        Pending = 3,
    }
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Material { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Piece { get; set; }

        [Required]
        public ProductStatuses Status { get; set; }

        //VARSA
        [MaxLength(500)]
        public string Description { get; set; }


        //BAĞLANTILAR

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public int CompanyID { get; set; }
        public Company Company { get; set; }

    }
}
