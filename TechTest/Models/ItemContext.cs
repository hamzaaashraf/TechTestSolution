using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TechTest.Models
{
    public class ItemContext : DbContext
    {
        public ItemContext() : base("name=ItemContext")
        {
        }
        public virtual DbSet<Item> Items { get; set; }
    }
}