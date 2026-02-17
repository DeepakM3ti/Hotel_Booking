using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otelier.BookingService.Data;
using Otelier.BookingService.DTOs;
using Otelier.BookingService.Models;


namespace Otelier.BookingService.Controllers
{
    [Route("api/hotels/{hotelId}/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetBookings(
            int hotelId,
            DateTime? startDate,
            DateTime? endDate)
        {
            var query = _context.Bookings
                .Where(b => b.HotelId == hotelId);

            if (startDate.HasValue)
                query = query.Where(b => b.CheckInDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(b => b.CheckOutDate <= endDate.Value);

            var bookings = await query.ToListAsync();

            if (!bookings.Any())
                return NotFound("No bookings found.");

            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(int hotelId, CreateBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.CheckOutDate <= dto.CheckInDate)
                return BadRequest("Check-out date must be greater than check-in date.");

            var hotelExists = await _context.Hotels.AnyAsync(h => h.Id == hotelId);
            if (!hotelExists)
                return NotFound("Hotel not found.");

            var isConflict = await _context.Bookings.AnyAsync(b =>
                b.HotelId == hotelId &&
                dto.CheckInDate < b.CheckOutDate &&
                dto.CheckOutDate > b.CheckInDate);

            if (isConflict)
                return BadRequest("Booking dates conflict with existing booking.");

            var booking = new Booking
            {
                HotelId = hotelId,
                GuestName = dto.GuestName,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow
                Hotel
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }

    }
}
