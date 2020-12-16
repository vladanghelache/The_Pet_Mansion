using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Pet_Mansion.Models
{
    public class Class1
    {
        private Product product = new Product();
        public HttpPostedFileBase UploadedFile { get; set; }
        public Product Product { get => product; set => product = value; }
    }
}