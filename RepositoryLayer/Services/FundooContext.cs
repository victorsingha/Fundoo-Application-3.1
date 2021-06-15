using CommonLayer;
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
        public DbSet<User> Users { get; set; }
    }
}
