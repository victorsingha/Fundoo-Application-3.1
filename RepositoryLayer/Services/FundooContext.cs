using CommonLayer;
using CommonLayer.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many To Many [Note Label]
            modelBuilder.Entity<Note_Label>()
                .HasOne(n => n.Note)
                .WithMany(l => l.Note_Labels)
                .HasForeignKey(nl => nl.NoteId);

            modelBuilder.Entity<Note_Label>()
                .HasOne(n => n.Label)
                .WithMany(l => l.Note_Labels)
                .HasForeignKey(nl => nl.LabelId);

            // Many To Many [User Label]
            modelBuilder.Entity<User_Label>()
                .HasOne(n => n.User)
                .WithMany(l => l.User_Labels)
                .HasForeignKey(nl => nl.UserId);

            modelBuilder.Entity<User_Label>()
               .HasOne(n => n.Label)
               .WithMany(l => l.User_Labels)
               .HasForeignKey(nl => nl.LabelId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Note_Label> Note_Labels { get; set; }
        public DbSet<User_Label> User_Labels { get; set; }

    }
}
