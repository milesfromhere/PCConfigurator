using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PCConfigurator
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Specification> Specifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PCComponentsDB;Trusted_Connection=True;");
        }
    }

    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class Component
    {
        public int ComponentID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public Category Category { get; set; }
        public List<Specification> Specifications { get; set; } = new();
    }

    public class Specification
    {
        public int SpecificationID { get; set; }
        public int ComponentID { get; set; }
        public string SpecText { get; set; }
        public Component Component { get; set; }
    }
}