using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synotech.SMP.Domain.Entities;

namespace Synotech.SMP.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Parents> Parents { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UsersInRoles> UsersInRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<Employees>().ToTable("Employees");
            modelBuilder.Entity<Parents>().ToTable("Parents");
            modelBuilder.Entity<Students>().ToTable("Students");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<UsersInRoles>().ToTable("UsersInRoles");
        }


        }
}
