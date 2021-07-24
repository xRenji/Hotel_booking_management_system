using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ThreeSistersHotel.Data;
using ThreeSistersHotel.Models;

namespace ThreeSistersHotel.Pages
{
    [Authorize(Roles = "customers")]
    public class BookRoomModel : PageModel
    {
        private readonly ThreeSistersHotel.Data.ApplicationDbContext _context;

        public BookRoomModel(ThreeSistersHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Room> DiffRooms { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            
            
            // retrieve the logged-in user's email
            
            string _email = User.FindFirst(ClaimTypes.Name).Value;

            Customer customer = await _context.Customer.FirstOrDefaultAsync(m => m.Email == _email);
            return Page();
        }

        [BindProperty]
        public BookRoomViewModel BookRoomViewModel { get; set; }
        

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Get email of logged in user
            string _email = User.FindFirst(ClaimTypes.Name).Value;

            Customer customer = await _context.Customer.FirstOrDefaultAsync(m => m.Email == _email);

          
            // Query


            // prepare the parameters to be inserted into the query
            var checkIn = new SqliteParameter("checkIn", BookRoomViewModel.CheckIn);
            var checkOut = new SqliteParameter("checkOut", BookRoomViewModel.CheckOut);
            var roomID = new SqliteParameter("room", BookRoomViewModel.ID);

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

            { // creating a booking object for inserting into database
                Booking booking = new Booking();
                // Construct this booking object based on the form inputs
                booking.RoomID = BookRoomViewModel.ID;
                booking.CheckIn = BookRoomViewModel.CheckIn;
                booking.CheckOut = BookRoomViewModel.CheckOut;
                // retrieve the booked room to get its price
                var theRoom = await _context.Room.FindAsync(BookRoomViewModel.ID);


                //Get the days from the dates inputs
                var days = (BookRoomViewModel.CheckOut - BookRoomViewModel.CheckIn).Days;

                // calculate the total price of this booking
                booking.Cost = days * theRoom.Price;


                //Get email of logged in user
                booking.CustomerEmail = _email;

                // Setting viewData for successful booking
                ViewData["Cost"] = booking.Cost;
                ViewData["Room"] = BookRoomViewModel.ID;
                ViewData["CheckIn"] = BookRoomViewModel.CheckIn;
                ViewData["CheckOut"] = BookRoomViewModel.CheckOut;
                ViewData["Level"] = theRoom.Level;

                _context.Booking.Add(booking);
                await _context.SaveChangesAsync();
                return Page();
            } else {
                ViewData["error"] = true; // Flag for error
                return Page();
            }

           
        }
    }
}
