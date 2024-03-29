using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;
using WebAppUni.Entities;

namespace WebAppUni.Repositories
{
    public class DataBase : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public DataBase()
        {
            this.Users = this.Set<User>();
            this.Cars = this.Set<Car>();
            this.Tasks = this.Set<Task>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=WebUniUsers;User Id=nmargenov;Password=123123;Encrypt=false")
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                Username = "admin",
                Password = "admin",
                FirstName = "admin",
                LastName = "admin",
                Role = (User.PermissionLevel)2
            });
        }
    }
}