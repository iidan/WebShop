using Avoda1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avoda1.ModelView
{
    public class CustomerViewModel
    {
        public Customer customer { get; set; }

        public List<Customer> customers { get; set; }
    }
}