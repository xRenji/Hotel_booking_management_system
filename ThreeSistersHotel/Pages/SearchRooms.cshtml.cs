using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ThreeSistersHotel.Models;

namespace ThreeSistersHotel.Pages
{
    [Authorize(Roles = "customers")]

    public class SearchRoomsModel : PageModel
    {
        private readonly ThreeSistersHotel.Data.ApplicationDbContext _context;

    public SearchRoomsModel (ThreeSistersHotel.Data.ApplicationDbContext context)
    {
        _context = context;
    }
        [BindProperty]
        
        public SearchRoom SearchInput { get; set; }

        // List of different rooms; for passing to Content file to display
        public IList<Room> DiffRooms { get; set; }
        

        
        public IActionResult OnGet()
        {
           
           
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // prepare the parameters to be inserted into the query
            var checkIn = new SqliteParameter("checkIn", SearchInput.CheckIn);
            var checkOut = new SqliteParameter("checkOut", SearchInput.CheckOut);
            var bedCount = new SqliteParameter("bedCount", SearchInput.BedCount);

            // Construct the query to get the rooms available
            // Use placeholders as the parameters
            var diffRooms = _context.Room.FromSqlRaw("select [Room].* from [Room]" +
                             " where [Room].BedCount = @bedCount and "
                             + "[Room].ID not in (select [Room].ID from [Room] inner join [Booking] on "
                             + "[Room].ID = [Booking].RoomID where [Booking].CheckIn < @checkOut and @checkIn < [Booking].CheckOut)", bedCount,checkIn,checkOut);


            // Run the query and save the results in DiffRooms for passing to content file
            DiffRooms = await diffRooms.ToListAsync();
            // Save the options for both dropdown lists in ViewData for passing to content file
            ViewData["SearchList"] = new SelectList(_context.Room, "ID", "Level", "BedCount", "Price");
            // invoke the content file
            return Page();
        }





        /*public void OnGet()
        {

        }*/
    }
}