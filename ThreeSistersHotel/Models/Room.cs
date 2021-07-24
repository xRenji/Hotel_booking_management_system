using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ThreeSistersHotel.Models
{
    public class Room
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"[gG1-3]{1}", ErrorMessage = "Can only consists of G or 1,2,3.")]
        public String Level { get; set; }

        [Range(1,3)]
        public int BedCount { get; set; }

       [Range(50.00,300.00)]
        public decimal Price { get; set; }


        //Navigation properties
        public ICollection<Booking> TheBookings { get; set; }
    }
}
