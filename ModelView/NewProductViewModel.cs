using Avoda1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avoda1.ModelView
{
    public class NewProductViewModel
    {
        public NewProduct newproduct { get; set; }

        public List<NewProduct> newproducts { get; set; }
    }
}