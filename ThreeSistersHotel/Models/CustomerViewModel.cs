using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSistersHotel.Models
{
    public class CustomerViewModel
    {
        [Required]
        [Display(Name = "Given Name")]
        [RegularExpression(@"[A-Za-z'-]{2,20}", ErrorMessage = "Can only consists of English letters, hyphen and apostrophe, and length must be between 2 characters and 20 characters inclusive")]
        public string GivenName { get; set; }

        [Required, Display(Name = "Surname")]
        [RegularExpression(@"[A-Za-z'-]{2,20}", ErrorMessage = "Can only consists of English letters, hyphen and apostrophe, and length must be between 2 characters and 20 characters inclusive")]
        public string SurName { get; set; }


        [Required]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "The postcode should be of four digits e.g 2134 .")]

        public string Postcode { get; set; }
    }
}
