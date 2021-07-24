using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThreeSistersHotel.Models
{
    public class Booking
    {
        //Primary key
        public int ID { get; set; }


        //foreign key Room entity
        public int RoomID { get; set; }

        //foreign key customer entity
        [Required]
        [DataType(DataType.EmailAddress)]

        public string CustomerEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
        
        public decimal Cost { get; set; }

        //Navigation properties

        public Room TheRoom { get; set; }
        public Customer TheCustomer { get; set; }

    }
}
