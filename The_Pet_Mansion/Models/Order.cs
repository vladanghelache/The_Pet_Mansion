using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Pet_Mansion.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public string UserID { get; set; }
        public DateTime Date { get; set; }
        
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}