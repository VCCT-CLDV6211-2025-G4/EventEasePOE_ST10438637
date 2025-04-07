using EventEasePOE.Controllers;
using System.ComponentModel.DataAnnotations;

namespace EventEasePOE.Models
{
    public class VenueM
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        public string VenueName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string ImageUrl { get; set; }

        //Navigation Properties
        public ICollection<BookingsM> Bookings { get; set; } 

    }
}
