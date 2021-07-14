using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prs_Web_Api.Models;

namespace Prs_Web_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Prs_Web_Api.Models.LineItem> LineItem { get; set; }

        public DbSet<Prs_Web_Api.Models.Product> Product { get; set; }

        public DbSet<Prs_Web_Api.Models.Request> Request { get; set; }

        public DbSet<Prs_Web_Api.Models.Users> Users { get; set; }

        public DbSet<Prs_Web_Api.Models.Vendor> Vendor { get; set; }
    }
}
