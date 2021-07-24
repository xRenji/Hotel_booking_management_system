using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThreeSistersHotel.Models;

namespace ThreeSistersHotel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ThreeSistersHotel.Models.Room> Room { get; set; }
        public DbSet<ThreeSistersHotel.Models.Customer> Customer { get; set; }
        public DbSet<ThreeSistersHotel.Models.Booking> Booking { get; set; }
       
    }
}
