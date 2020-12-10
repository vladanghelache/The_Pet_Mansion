using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace The_Pet_Mansion.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Numele produsului este obligatoriu!")]
        public string ProductName { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Descrierea produsului este obligatorie!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Cantitatea produsului trebuie sa fie specificata!")]
        [Range(1, int.MaxValue, ErrorMessage = "Cantitatea produsului trebuie sa fie cel putin egala cu 1!")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Pretul produsului trebuie sa fie specificat!")]
        [Range(0.05, float.MaxValue, ErrorMessage = "Pretul produsului trebuie sa fie cel putin egal cu 0.05!")]
        public float Price { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Categoria trebuie selectata!")]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Tipul animalului trebuie selectat!")]
        public int AnimalID { get; set; }
        public string UserID { get; set; }


        public virtual ICollection <Review> Reviews { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual ICollection<CartLine> CartLines { get; set; }

        public virtual Category Category { get; set; }
        public virtual Animal Animal { get; set; }
        public virtual ApplicationUser User { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Animals { get; set; }



    }
}