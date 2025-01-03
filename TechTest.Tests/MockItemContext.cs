using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechTest.Models;

namespace TechTest.Tests
{
    public class MockItemContext : ItemContext
    {
        public Mock<DbSet<Item>> MockItems { get; set; }

        public MockItemContext()
        {
            MockItems = new Mock<DbSet<Item>>();

            var items = new List<Item>
            {
                new Item { Id = 1, Name = "Item 1", Description = "Description 1", Price = 100, IsActive = true },
                new Item { Id = 2, Name = "Item 2", Description = "Description 2", Price = 200, IsActive = true }
            }.AsQueryable();

            MockItems.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(items.Provider);
            MockItems.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(items.Expression);
            MockItems.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(items.ElementType);
            MockItems.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

            this.Items = MockItems.Object;
        }

        public override DbSet<Item> Items { get; set; }
    }
}
