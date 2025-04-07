using EventEasePOE.Controllers;
using System.ComponentModel.DataAnnotations;

namespace EventEasePOE.Models
{
    public class EventsM
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }

        public ICollection <BookingsM> Bookings { get; set; } 
    }
}
