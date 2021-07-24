using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSistersHotel.Models
{
    public class Statistics
    {
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }
        [Display(Name = "Customers Count")]
        public int CustomersCount { get; set; }
        
        [Display(Name = "Bookings Count")]
        public int BookingsCount { get; set; }

        [Display(Name = "Rooms")]
        public int Room { get; set; }

    }
}
