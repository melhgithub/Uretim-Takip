using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public enum CustomerNoteStatuses
    {
        // Status.Removed.DisplayName(); //KALDIRILDI ŞEKLİNDE KULLANILACAK

        [Description("AKTİF")]
        Approved = 1,

        [Description("KALDIRILDI")]
        Removed = 2,
    }
    public class CustomerNote
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Note { get; set; }
        [Required]
        public CustomerNoteStatuses Status { get; set; }



        //BAĞLANTILAR

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }



        public int CompanyID { get; set; }
        public Company Company { get; set; }

    }
}
