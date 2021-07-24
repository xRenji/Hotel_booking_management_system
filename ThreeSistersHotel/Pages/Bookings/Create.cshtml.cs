using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ThreeSistersHotel.Data;
using ThreeSistersHotel.Models;

namespace ThreeSistersHotel.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly ThreeSistersHotel.Data.ApplicationDbContext _context;

        public CreateModel(ThreeSistersHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Room> DiffRooms { get; set; }

        public IActionResult OnGet()
        {
        ViewData["CustomerEmail"] = new SelectList(_context.Customer, "Email", "FullName");
        ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
           
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // prepare the parameters to be inserted into the query
            var checkIn = new SqliteParameter("checkIn", Booking.CheckIn);
            var checkOut = new SqliteParameter("checkOut", Booking.CheckOut);
            var roomID = new SqliteParameter("room", Booking.RoomID);

            // Construct the query to get the rooms available
            // Use placeholders as the parameters
            var diffRooms = _context.Room.FromSqlRaw("select [Room].* from [Room]" +
                             " where [Room].ID = @room and "
                             + "[Room].ID not in (select [Room].ID from [Room] inner join [Booking] on "
                             + "[Room].ID = [Booking].RoomID where [Booking].CheckIn < @checkOut and @checkIn < [Booking].CheckOut)", roomID, checkIn, checkOut);


            // Run the query and save the results in DiffRooms for passing to content file
            DiffRooms = await diffRooms.ToListAsync();


            // ADD ENTRY INTO DATABASE

            if (DiffRooms.Count != 0)
            {

                _context.Booking.Add(Booking);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            } else
            {
                ViewData["error"] = true; // Flag for error
                return Page();
            }
        }
    }
}
