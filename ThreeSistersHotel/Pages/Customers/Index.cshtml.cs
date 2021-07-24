using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;

using ThreeSistersHotel.Data;

using ThreeSistersHotel.Models;



namespace ThreeSistersHotel.Pages.Customers

{

    public class IndexModel : PageModel

    {

        private readonly ThreeSistersHotel.Data.ApplicationDbContext _context;



        public IndexModel(ThreeSistersHotel.Data.ApplicationDbContext context)

        {

            _context = context;

        }



        public IList<Customer> Customer { get; set; }



        public async Task OnGetAsync()

        {

            Customer = await _context.Customer.ToListAsync();

        }

    }

}