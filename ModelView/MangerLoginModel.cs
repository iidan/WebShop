using Avoda1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avoda1.ModelView
{
    public class MangerLoginModel
    {
        public Manger user { get; set; }

        public List<Manger> users { get; set; }
        
    }
}