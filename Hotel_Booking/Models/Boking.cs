using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otelier.BookingService.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        [Required]
        [MaxLength(200)]
        public string GuestName { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public Hotel Hotel { get; set; }
    }
}
