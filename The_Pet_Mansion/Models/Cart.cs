using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace The_Pet_Mansion.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        [Required]
        public string UserID { get; set; }
        
        

        public virtual ICollection<CartLine> CartLines { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}