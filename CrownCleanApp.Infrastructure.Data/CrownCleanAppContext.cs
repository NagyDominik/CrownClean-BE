using CrownCleanApp.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Infrastructure.Data
{
    public class CrownCleanAppContext : DbContext
    {
        public CrownCleanAppContext(DbContextOptions<CrownCleanAppContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .Property(p => p.Addresses)
                .HasConversion(
                    a => JsonConvert.SerializeObject(a),
                    a => JsonConvert.DeserializeObject<List<string>>(a)
                );

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Vehicles)
            //    .WithOne(v => v.User)
            //    .OnDelete(DeleteBehavior.SetNull);

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
