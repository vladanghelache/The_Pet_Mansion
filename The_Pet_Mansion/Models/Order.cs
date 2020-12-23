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
        [Required(ErrorMessage = "Adresa este camp obligatoriu!")]
        public string Address { get; set; }
        [MinLength(10, ErrorMessage = "Numarul trebuie sa contina 10 cifre!")]
        [MaxLength(10, ErrorMessage = "Numarul trebuie sa contina 10 cifre!")]
        [Required(ErrorMessage = "Numarul de telefon e obligatoriu!")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}