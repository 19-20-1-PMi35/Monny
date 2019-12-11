using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;

namespace DataAccess
{
    public class MonnyDbContext: DbContext
    {
        public MonnyDbContext() :
            base(){ }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Dream> Dreams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var categories = new List<Category>
            {
                new Category {Id = 1, Name = "Food"},
                new Category {Id = 2, Name = "Health"},
                new Category {Id = 3, Name = "Traveling"},
                new Category {Id = 4, Name = "Clothes"},
                new Category {Id = 5, Name = "Cafes"},
                new Category {Id = 6, Name = "Transport"},
                new Category {Id = 7, Name = "Entertainments"},
                new Category {Id = 8, Name = "Salary", CategoryType = 1},
                new Category {Id = 9, Name = "Scholarship", CategoryType = 1},
                new Category {Id = 10, Name = "Retirement", CategoryType = 1},
                new Category {Id = 11, Name = "Rent", CategoryType = 2},
                new Category {Id = 12, Name = "Part-time job", CategoryType = 2},
                new Category {Id = 13, Name = "Shares", CategoryType = 2}
            };

            modelBuilder.Entity<Category>().HasData(categories);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MonnyDataBase;Trusted_Connection=True;");
        }
    }
}
