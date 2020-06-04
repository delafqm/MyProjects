using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreWebApp2._2.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() : base()
        { }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<User> User { get; set; }
    }
}
