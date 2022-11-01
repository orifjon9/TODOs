using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TODOs.Data.Entities;

namespace TODOs.Data
{
    public class TodoDbContext: DbContext
    {
        /*private readonly string _connectionString;
        public TodoDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }*/
        public TodoDbContext() { }
        public TodoDbContext(DbContextOptions<TodoDbContext> options): base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Entities.List> Lists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.List>().HasData(new List<Entities.List>()
            { 
                new Entities.List
                {
                    Id = 1,
                    Label = "Morning",
                },
                new Entities.List
                {
                    Id = 2,
                    Label = "Evening"
                }
            });


            modelBuilder.Entity<Todo>().HasData(new List<Todo>
            {
                new Todo
                {
                    Id = 1,
                    Label = "Brush teeth",
                    ListId = 1
                },
                new Todo
                {
                    Id = 2,
                    Label = "Breakfast",
                    ListId = 1
                },
                new Todo
                {
                    Id = 3,
                    Label = "Dress up",
                    ListId = 1
                },
                new Todo
                {
                    Id = 4,
                    Label = "Spend time with family",
                    ListId = 2
                },
                new Todo
                {
                    Id = 5,
                    Label = "Have a dinner",
                    ListId = 2
                },
                new Todo
                {
                    Id = 6,
                    Label = "Watch TV",
                    ListId = 2
                }
            });
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlServer("Server=localhost,1439; Database=TODO;User=sa; Password=todos-123");
    }
}

