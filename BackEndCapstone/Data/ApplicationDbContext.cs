using System;
using System.Collections.Generic;
using System.Text;
using BackEndCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackEndCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeTicket> EmployeeTickets { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Test1!");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<Department>().HasData(
            new Department()
            {
                DepartmentId = 1,
                Name = "Shoes"
            },
            new Department()
            {
                DepartmentId = 2,
                Name = "Gear"
            },
            new Department()
            {
                DepartmentId = 3,
                Name = "Men's Apparel"
            },
            new Department()
            {
                DepartmentId = 4,
                Name = "Women's Apparel"
            },
            new Department()
            {
                DepartmentId = 5,
                Name = "POS"
            }
            );

            modelBuilder.Entity<Employee>().HasData(
               new Employee()
               {
                   EmployeeId = 1,
                   FirstName = "Tommy",
                   LastName = "Allen",
                   PhoneNumber = "615-123-4567"
               },
               new Employee()
               {
                   EmployeeId = 2,
                   FirstName = "Zack",
                   LastName = "Huntington",
                   PhoneNumber = "615-765-4321"
               }
           );

            modelBuilder.Entity<Ticket>().HasData(
               new Ticket()
               {
                   TicketId = 1,
                   Title = "Fix Shoes",
                   Description = "Go through all shoes that are displayed on the back wall and tuck shoe laces into shoes to make a cleaner look.",
                   DepartmentId = 1,
                   EmployeeId = null,
                   ApplicationUserId = user.Id,
                   IsComplete = false
               },
               new Ticket()
               {
                   TicketId = 2,
                   Title = "Organize Rope",
                   Description = "Go through all climbing rope and organize by length, static, and dynamic.",
                   DepartmentId = 2,
                   EmployeeId = null,
                   ApplicationUserId = user.Id,
                   IsComplete = false
               }
           );

            modelBuilder.Entity<EmployeeTicket>().HasData(
                new EmployeeTicket()
                {
                    EmployeeTicketId = 1,
                    EmployeeId = 1,
                    TicketId = 1
                },
                 new EmployeeTicket()
                 {
                     EmployeeTicketId = 2,
                     EmployeeId = 2,
                     TicketId = 2
                 }
            );
        }
    }
}

