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
        public string CartID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Cantitatea este un camp obligatoriu!")]
        [Range(1, int.MaxValue, ErrorMessage = "Cantitatea produsului trebuie sa fie cel putin egala cu 1!")]
        public int Quantity { get; set; }

        public virtual Cart Cart{ get; set; }
        public virtual Product Product { get; set; }
    }
}