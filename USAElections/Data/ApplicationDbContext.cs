using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USAElections.Models;

namespace USAElections.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<Constituency> Constituency { get; set; }
        public DbSet<CandidateConstituency> CandidateConstituency { get; set; }
        public DbSet<Vote> Vote { get; set; }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API for many to many
            modelBuilder.Entity<CandidateConstituency>()
                .HasOne(c => c.Candidate)
                .WithMany(x => x.CandidateConstituency)
                .HasForeignKey(ci => ci.CandidateId);

            modelBuilder.Entity<CandidateConstituency>()
                .HasOne(c => c.Constituency)
                .WithMany(x => x.CandidateConstituency)
                .HasForeignKey(ci => ci.ConstituencyId);

            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<CandidateConstituency>()
            //     .HasKey(x => new { x.CandidateId, x.ConstituencyId });

            // modelBuilder.Entity<Vote>()
            //.HasOne(n => n.Candidate)
            //.WithMany(n => n.Vote)
            //.HasForeignKey(n => n.CandidateId)
            //.OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
