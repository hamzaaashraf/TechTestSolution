namespace TechTest.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TechTest.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TechTest.Models.ItemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TechTest.Models.ItemContext";
        }
        
        protected override void Seed(ItemContext context)
        {
            context.Items.AddOrUpdate(
                new Item
                {
                    Name = "Gaming Laptop",
                    Description = "High-end laptop with powerful graphics card.",
                    Price = 1200,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                },
                new Item
                {
                    Name = "Smartphone",
                    Description = "Latest generation smartphone with OLED display.",
                    Price = 800,
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-2)
                },
                new Item
                {
                    Name = "Wireless Headphones",
                    Description = "Noise-canceling over-ear headphones.",
                    Price = 200,
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new Item
                {
                    Name = "Smartwatch",
                    Description = "Fitness tracker with heart rate monitoring.",
                    Price = 150,
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-7)
                },
                new Item
                {
                    Name = "Bluetooth Speaker",
                    Description = "Portable speaker with high-quality sound.",
                    Price = 120,
                    IsActive = false,
                    CreatedAt = DateTime.Now.AddDays(-10)
                },
                new Item
                {
                    Name = "Tablet",
                    Description = "10-inch tablet for media consumption.",
                    Price = 400,
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-15)
                },
                new Item
                {
                    Name = "External Hard Drive",
                    Description = "1TB USB 3.0 external hard drive.",
                    Price = 100,
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-20)
                },
                new Item
                {
                    Name = "Gaming Mouse",
                    Description = "Ergonomic mouse with customizable buttons.",
                    Price = 50,
                    IsActive = false,
                    CreatedAt = DateTime.Now.AddDays(-25)
                },
                new Item
                {
                    Name = "Mechanical Keyboard",
                    Description = "RGB keyboard with tactile feedback.",
                    Price = 80,
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-30)
                },
                new Item
                {
                    Name = "4K Monitor",
                    Description = "27-inch monitor with UHD resolution.",
                    Price = 500,
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-35)
                }
            );
        }

    }
}
