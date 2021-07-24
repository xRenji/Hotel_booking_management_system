using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThreeSistersHotel.Data;
using ThreeSistersHotel.Models;

namespace ThreeSistersHotel.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly ThreeSistersHotel.Data.ApplicationDbContext _context;

        public IndexModel(ThreeSistersHotel.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; }

        public async Task OnGetAsync()
        {
            Booking = await _context.Booking
                .Include(b => b.TheCustomer)
                .Include(b => b.TheRoom).ToListAsync();
        }
    }
}
