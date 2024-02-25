using HotelAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using System.Collections.Generic;

namespace HotelAPI.Tests
{
    public class HotelsControllerTests
    {
        private HotelController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new HotelController(new MemoryCache(new MemoryCacheOptions()));
        }

        [Test]
        public void GetRoomsForHotel_ReturnsOkResult()
        {
            // Arrange
            string hotelCode = "SPRIN";

            // Act
            var result = _controller.GetRooms(hotelCode);

            // Assert
            Assert.That(result is OkObjectResult);
        }

        [Test]
        public void GetCheapestHotel_ReturnsOkResult()
        {
            // Arrange
            string roomType = "Double Room Standard";

            // Act
            var result = _controller.GetCheapestHotel(roomType);

            // Assert
            Assert.That(result is OkObjectResult);
        }

        [Test]
        public void GetHotelsInCity_ReturnsOkResult()
        {
            // Arrange
            string city = "Springfield";

            // Act
            var result = _controller.GetHotelsInCity(city);

            // Assert
            Assert.That(result is OkObjectResult);
        }
    }
}

