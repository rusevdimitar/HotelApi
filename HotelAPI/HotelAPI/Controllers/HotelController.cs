// using Contracts;
using HotelAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HotelAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private TravelAgency _travelAgency;

        private readonly IMemoryCache _memoryCache;

        //private readonly ILoggerManager _logger;

        public HotelController(IMemoryCache memoryCache /*, ILoggerManager logger */)
        {
            _memoryCache = memoryCache;
            //_logger = logger;
            LoadData();
        }

        [HttpGet("rooms/{hotelCode}")]
        public IActionResult GetRooms(string hotelCode)
        {           
            var roomsInHotel = _travelAgency.Hotels.FirstOrDefault(h => h.Code == hotelCode).GuestRooms.ToList();

            return Ok(roomsInHotel);
        }

        [HttpGet("cheapesthotel/{roomType}")]
        public IActionResult GetCheapestHotel(string roomType)
        {
            var cheapestHotel = _travelAgency.Hotels.SelectMany(h => h.GuestRooms).Where(gr => gr.Room == roomType).OrderBy(room => room.PricePerNight).FirstOrDefault()?.HotelCode;

            return Ok(cheapestHotel);
        }

        [HttpGet("hotels/{city}")]
        public IActionResult GetHotelsInCity(string city)
        {
            var cacheData = _memoryCache.Get<IEnumerable<Hotel>>(nameof(GetHotelsInCity));

            if (cacheData != null)
            {
                return Ok(cacheData);
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            cacheData = _travelAgency.Hotels.Where(h => h.City == city).OrderByDescending(h => h.LocalCategory).ToList();
            _memoryCache.Set(nameof(GetHotelsInCity), cacheData, expirationTime);

            return Ok(cacheData);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult HandleError()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            //_logger.LogError(exceptionHandlerFeature.Error.Message);
            return Problem();
        }

        private void LoadData()
        {
            string hotelDataJsonFilePath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["hotelDataJsonFilePath"];

            string jsonString = System.IO.File.ReadAllText(hotelDataJsonFilePath ?? "hoteldata.json");

            _travelAgency =
                JsonSerializer.Deserialize<TravelAgency>(jsonString, new JsonSerializerOptions()
                {
                    AllowTrailingCommas = true
                });

            if (_travelAgency != null && _travelAgency.Hotels.Any())
            {
                foreach (Hotel hotel in _travelAgency.Hotels)
                {
                    foreach (GuestRoom guestRoom in hotel.GuestRooms)
                    {
                        guestRoom.HotelCode = hotel.Code;
                    }
                }
            }
        }
    }
}