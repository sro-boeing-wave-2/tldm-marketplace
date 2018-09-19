using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceBackend.Models
{
    public class MarketPlaceBackendContext : DbContext
    {
        public MarketPlaceBackendContext (DbContextOptions<MarketPlaceBackendContext> options)
            : base(options)
        {
        }

        public DbSet<MarketPlaceBackend.Models.Application> Application { get; set; }
    }
}
