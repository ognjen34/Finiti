using Finiti.DOMAIN.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Finiti.DATA
{
    
        public class DatabaseContext : DbContext
        {
            public DbSet<Author> Authors { get; set; }


        public DatabaseContext(DbContextOptions options) : base(options)
            {

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

               

            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseLazyLoadingProxies();


            }
        }  
}
