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
        [Key]        [ForeignKey("User")]        public string CartID { get; set; }

        public virtual ICollection<CartLine> CartLines { get; set; }        public virtual ApplicationUser User { get; set; }

    }
}