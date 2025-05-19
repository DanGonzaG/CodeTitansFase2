using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.AD
{
    public class ContextoAndy : DbContext
    {
        public ContextoAndy(DbContextOptions<ContextoAndy> options) : base(options) { }

        //public DbSet<>


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}