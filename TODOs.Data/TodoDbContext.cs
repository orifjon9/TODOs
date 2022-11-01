using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TODOs.Data.Entities;

namespace TODOs.Data
{
    public class TodoDbContext: DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options): base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoList> Lists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder.Entity<List>(entity =>
            {
                entity.Property(e => e.Label).HasMaxLength(128).IsRequired();
                //entity.HasMany(e => e.Todos).WithOne(e => e.List).HasForeignKey(e => e.Id);

                /*entity.HasData(new List
                {
                    Id = 1,
                    Label = "Morning",
                    Todos = new List<Todo>
                    {
                        new Todo
                        {
                            Id = 1,
                            Label = "Brush teeth"
                        },
                        new Todo
                        {
                            Id = 2,
                            Label = "Breakfast"
                        },
                        new Todo
                        {
                            Id = 3,
                            Label = "Dress up"
                        }
                    }
                },
                new List
                {
                    Id = 2,
                    Label = "Evening"
                });
            });



            modelBuilder.Entity<Todo>(entity =>
            {
                entity.Property(e => e.Label).HasMaxLength(256).IsRequired();
                //entity.HasOne(e => e.List).WithMany(e => e.Todos).HasForeignKey(e => e.Id);
            });*/
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Server=localhost,1439;Initial Catalog=TODO;Database=TODO;User Id=sa;Password=todos-123;");
        // }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433; Database=TODO;User=sa; Password=TODOPassword");
    }
}

