using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Avoda1.Models
{
    public class NewProduct
    {
        [Required]
        [RegularExpression("^[a-z,A-Z]*$", ErrorMessage = "*Your ProductName have to words.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Enter product name between 2 and 20 words")]
        public string ProductName { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "*Your Price Id have to be numbers.")]
        public int Price { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "*Enter Amount between 2 and 20 numers")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "*Your InitialQuantity have to be numbers.")]
        public string InitialQuantity { get; set; }

        [Key]
        [Required]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "*Your Product Id have to be 5 numbers.")]
        public string ProductId { get; set; }
    }
}