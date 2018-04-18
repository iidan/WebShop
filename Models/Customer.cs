using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Avoda1.Models
{
    public class Customer
    {
        [Required]
        [RegularExpression("^[a-z,A-Z]*$", ErrorMessage = "*Your ProductName have to words.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Enter your name between 2 and 20 words")]
        public string ProductName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "*Enter Price between 2 and 20 numers")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "*Your Price Id have to be numbers.")]
        public string Quantity { get; set; }

        [Required]
        [RegularExpression("^[a-z,A-Z]*$", ErrorMessage = "*Your ProductName have to words.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Enter your name between 2 and 20 words")]
        public string CustomerName { get; set; }

        [Key]
        [Required]
        public string Date { get; set; }

    }
}