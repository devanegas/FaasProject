using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FaasWeb.Models
{
    public class FaasWebContext : DbContext
    {
        public FaasWebContext (DbContextOptions<FaasWebContext> options)
            : base(options)
        {
        }

        public DbSet<FaasWeb.Models.Person> Person { get; set; }
    }
}
