using System.ComponentModel.DataAnnotations;

namespace Otelier.BookingService.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        // Navigation property
        public ICollection<Booking> Bookings { get; set; }
    }
}
