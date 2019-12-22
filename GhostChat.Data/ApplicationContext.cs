using GhostChat.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GhostChat.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) { }
    }
}
