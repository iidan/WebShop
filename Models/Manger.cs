using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Avoda1.Models
{
    public class Manger
    {
        [Required]
        [RegularExpression("^[a-z,A-Z]*$", ErrorMessage = "*Your ProductName have to words.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Enter your last name between 2 and 10 words")]
        public string Username { get; set; }

        [Key]
        [Required]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Your Password have to be 5 numbers.")]
        public string Password { get; set; }
    }
}