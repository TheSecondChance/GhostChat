using GhostChat.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GhostChat.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMessage>()
                .HasKey(k => new { k.MessageId, k.UserID });

            modelBuilder.Entity<UserMessage>()
                .HasOne(um => um.User)
                .WithMany(u => u.UserMessages)
                .HasForeignKey(um => um.UserID);

            modelBuilder.Entity<UserMessage>()
                .HasOne(um => um.Message)
                .WithMany(m => m.UserMessages)
                .HasForeignKey(um => um.MessageId);
        }
    }
}
