using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace The_Pet_Mansion.Models
{
    public class UploadFile
    {
        [Key]
        [ForeignKey("Product")]
        public int FileId { get; set; }


        public string FilePath { get; set; } // Calea unde se va salva fisierul
        public string FileName { get; set; } // Numele fisierului
        public string Extension { get; set; } // Extensia 

       

        public virtual Product Product { get; set; }

    }
}