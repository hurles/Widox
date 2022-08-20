using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widox.Models.DatabaseObjects;

namespace Widox.Database.Context
{
    public partial class WidoxDbContext : IdentityDbContext<WidoxUser, WidoxRole, long>
    {

        public WidoxDbContext(DbContextOptions<WidoxDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
