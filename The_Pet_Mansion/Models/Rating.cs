using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Pet_Mansion.Models
{
    public class Rating
    {
        [Key]
        public int RatingID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string ProductID { get; set; }
        [Required]
        public int Value { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }

    }
}