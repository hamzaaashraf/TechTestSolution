using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web.Mvc;
using TechTest.Controllers;
using TechTest.Models;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Http.Results;

namespace TechTest.Tests.Controllers
{
    [TestClass]
    public class ItemControllerTests
    {
        private ItemController _controller;
        private MockItemContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new MockItemContext();
            _controller = new ItemController(_context); // Using the mocked context
        }

        [TestMethod]
        public void Create_Post_Returns_RedirectToAction_When_Item_Is_Valid()
        {
            var item = new Item
            {
                Name = "New Item",
                Description = "New Description",
                Price = 150,
                IsActive = true
            };

            var result = _controller.Create(item) as System.Web.Mvc.RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
