using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using TechTest.Controllers;
using TechTest.Models;

namespace TechTest.Tests.Controllers
{
    [TestClass]
    public class ItemApiControllerTests
    {
        private ItemApiController _controller;
        private MockItemContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new MockItemContext();
            _controller = new ItemApiController(_context); // Using the mocked context
        }

        [TestMethod]
        public void CreateItem_Returns_Created_When_Item_Is_Valid()
        {
            var item = new Item
            {
                Name = "New Item",
                Description = "New Description",
                Price = 150,
                IsActive = true
            };

            var result = _controller.CreateItem(item) as CreatedAtRouteNegotiatedContentResult<Item>;

            Assert.IsNotNull(result);
            //Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual("DefaultApi", result.RouteName);
            Assert.AreEqual(item.Name, result.Content.Name);
        }
    }
}
