using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DutResLeague.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext() : base("DutResLeague") { }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);

            // Configurations
            modelBuilder.Entity<Club>()
                .HasRequired(c => c.Coach)
                .WithMany(u => u.Clubs)
                .HasForeignKey(c => c.CoachId);

            modelBuilder.Entity<Player>()
                .HasRequired(p => p.Club)
                .WithMany(c => c.Players)
                .HasForeignKey(p => p.ClubId);
        }
    }
}