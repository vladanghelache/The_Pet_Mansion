using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Pet_Mansion.Models
{
    public class CartLine
    {
        [Key]
        public int CartLineID { get; set; }
        [Required]
        public int CartID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }

        public virtual Cart Cart{ get; set; }
        public virtual Product Product { get; set; }
    }
}