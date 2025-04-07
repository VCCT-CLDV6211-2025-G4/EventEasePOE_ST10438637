using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEasePOE.Models
{
    public class BookingsM
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }


        //Foreign Keys

        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        //Navigation Properties
        public VenueM Venue { get; set; }
        public EventsM Event { get; set; }
    }
}
