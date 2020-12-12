using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Pet_Mansion.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        [Required(ErrorMessage = "Continutul review-ului nu poate fi gol!")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int ProductID { get; set; }
        public string UserID { get; set; }

        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}