﻿using KaarvensBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KaarvensBackend.Database
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<ProjectDetails> ProjectDetails { get; set; }

        public DbSet<DrawingDetails> DrawingsDetails { get; set; }

        public DbSet<UserDetails> UserDetails { get; set; }

        public DbSet<TaskDetails> TaskDetails { get; set; }

        public DbSet<FinanceDetails> FinanceDetails { get; set; }

        //while create database it will make primary key and foriegn key relationshipfor project details and drawings table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrawingDetails>()
                .HasOne(_ => _.ProjectDetails)
                .WithMany(_ => _.DrawingDetails)
                .HasForeignKey(_ => _.ProjectDetailsId);

            modelBuilder.Entity<UserDetails>()
                .HasIndex(u => u.Email)
                .IsUnique();

        }
    }
}
