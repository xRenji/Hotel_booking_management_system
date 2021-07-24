using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThreeSistersHotel.Models;

namespace ThreeSistersHotel.Pages
{   
    public class StatisticsModel : PageModel
    {

        private readonly ThreeSistersHotel.Data.ApplicationDbContext _context;
        public StatisticsModel(ThreeSistersHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // For passing the results to the Content file
        public IList<Statistics> CountStats { get; set; }
        public IList<Statistics> CountStatsRoom { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // divide the customers into groups by postcode
            var countGroups = _context.Customer.GroupBy(m => m.Postcode);

           // Query to select bookings by rooms
            var countStatsRoom = _context.Room.FromSqlRaw("SELECT Room.ID, count(Booking.ID) from Room left join Booking "
                            + "on Room.ID = Booking.RoomID group by Room.ID");
            
                          

            // for each group, get its postcode and the number of customers in this group
            CountStats = await countGroups.Select(g => new Statistics { Postcode = g.Key, CustomersCount = g.Count() }).ToListAsync();
            
            // for each group, get its room ID and the number of bookings in this group

            CountStatsRoom = await countStatsRoom.Select(g => new Statistics { Room = g.ID, BookingsCount = g.TheBookings.Count }).ToListAsync();


            return Page();
        }












        /*public void OnGet()
        {

        }*/
    }
}