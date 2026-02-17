using System.ComponentModel.DataAnnotations;

namespace Otelier.BookingService.DTOs
{
    public class CreateBookingDto
    {
        [Required]
        public string GuestName { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}
