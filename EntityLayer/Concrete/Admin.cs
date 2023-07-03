using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string AdminUserName { get; set; }
        [Required]
        [StringLength(50)]
        public string AdminPassword { get; set; }

    }
}
