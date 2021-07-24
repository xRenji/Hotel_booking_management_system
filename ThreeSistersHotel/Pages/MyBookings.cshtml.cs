using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThreeSistersHotel.Data;
using ThreeSistersHotel.Models;

namespace ThreeSistersHotel.Pages.Bookings
{
    [Authorize(Roles = "customers")]
    public class MyBookingsModel : PageModel
    {
        private readonly ThreeSistersHotel.Data.ApplicationDbContext _context;

        public MyBookingsModel(ThreeSistersHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Variables to manage sorting and filtering
        public string CheckInSort { get; set; }
        public string TotalSort { get; set; }
        
        public IList<Booking> Booking { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {

            // retrieve the logged-in user's email

            string _email = User.FindFirst(ClaimTypes.Name).Value;
            Customer customer = await _context.Customer.FirstOrDefaultAsync(m => m.Email == _email);


            // Varibales to get the sort from user link input
            CheckInSort = sortOrder == "checkin" ? "checkin_desc" : "checkin";
            TotalSort = sortOrder == "Total" ? "total_desc" : "Total";
           

            // Get the bookings
            Booking = await _context.Booking
                .Where(s => s.CustomerEmail == _email)
                .Include(b => b.TheCustomer)
                .Include(b => b.TheRoom).ToListAsync();



            switch (sortOrder)
            {
                case "checkin_desc":
                    Booking = await _context.Booking
                    .OrderByDescending(s => s.CheckIn)
                    .Where(s => s.CustomerEmail == _email)
                    .Include(b => b.TheCustomer)
                    .Include(b => b.TheRoom).ToListAsync();
                    break;
                case "checkin":
                    Booking = await _context.Booking
                    .OrderBy(s => s.CheckIn)
                    .Where(s => s.CustomerEmail == _email)
                    .Include(b => b.TheCustomer)
                    .Include(b => b.TheRoom).ToListAsync();
                    break;
                case "total_desc":
                    Booking = await _context.Booking
                    .OrderByDescending(s => (int)s.Cost)
                    .Where(s => s.CustomerEmail == _email)
                    .Include(b => b.TheCustomer)
                    .Include(b => b.TheRoom).ToListAsync();
                    break;
                case "Total":
                    Booking = await _context.Booking
                    .OrderBy(s => (int)s.Cost)
                    .Where(s => s.CustomerEmail == _email)
                    .Include(b => b.TheCustomer)
                    .Include(b => b.TheRoom).ToListAsync();
                    break;
             


            }


        }
    }
}
