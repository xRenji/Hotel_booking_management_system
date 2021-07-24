using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThreeSistersHotel.Models
{
    public class Customer
    {
        [Key, Required]
        [DataType(DataType.EmailAddress)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Email { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        [RegularExpression(@"[A-Za-z'-]{2,20}", ErrorMessage = "Can only consists of English letters, hyphen and apostrophe, and length must be between 2 characters and 20 characters inclusive")]
        public string GivenName { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        [RegularExpression(@"[A-Za-z'-]{2,20}", ErrorMessage = "Can only consists of English letters, hyphen and apostrophe, and length must be between 2 characters and 20 characters inclusive")]
        public string SurName { get; set; }

        [NotMapped] // not mapping this property to database, but exist in memory
        public string FullName => $"{GivenName} {SurName}";

        [Required]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "The postcode should be of four digits e.g 2134 .")]

        public string Postcode { get; set; }
        
        
        //Navigation properties
        public ICollection<Booking> TheBookings { get; set; }


    }
}
