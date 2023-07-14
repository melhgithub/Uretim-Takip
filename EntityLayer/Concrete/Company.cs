using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public enum CompanyStatuses
    {
        // Status.Removed.DisplayName(); //KALDIRILDI ŞEKLİNDE KULLANILACAK

        [Description("AKTİF")]
        Approved = 1,

        [Description("KALDIRILDI")]
        Removed = 2
    }
    public class Company
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Mail { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        public CompanyStatuses Status { get; set; }

        //VARSA
        [MaxLength(500)]
        public string Description { get; set; }


        //BAĞLANTILAR

        public List<Product> Product { get; set; }
        public List<Offer> Offer { get; set; }
        public List<Order> Order { get; set; }
        public List<Production> Production { get; set; }
        public List<CompanyNote> CompanyNote { get; set; }


    }
}
