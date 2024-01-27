using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingApi.Models;
using HotelBookingApi.Data;
namespace HotelBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;

        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }

        //create/edit booking
        [HttpPost]
        public JsonResult CreateEdit(HotelBooking booking)
        {
            if (booking.Id == 0)
            {
                _context.HotelBookings.Add(booking);
            }
            else
            {
                var bookingInDb = _context.HotelBookings.Find(booking.Id);
                if (bookingInDb == null)
                {
                    return new JsonResult(NotFound());
                }
                bookingInDb.RoomNumber = booking.RoomNumber;
            }
            _context.SaveChanges();
            return new JsonResult(Ok(booking));
        }
        //Get by Id
        [HttpGet]
        public JsonResult Get(int Id)
        {
            var result = _context.HotelBookings.Find(Id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            return new JsonResult(Ok(result));
        }
        //Get all
        [HttpGet]
        public JsonResult GetAll()
        {
            return new JsonResult(Ok(_context.HotelBookings));
        }

        //Delete
        [HttpDelete]
        public JsonResult Delete(int Id)
        {
            var result = _context.HotelBookings.Find(Id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            _context.HotelBookings.Remove(result);
            _context.SaveChanges();
            return new JsonResult(Ok());
        }
    }
}
