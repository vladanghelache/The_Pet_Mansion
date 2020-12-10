using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Pet_Mansion.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Numele categoriei este obligatoriu!")]
        public String CategoryName { get; set; }

        public virtual ICollection<Product>Products { get; set; }
    }
}