using System.ComponentModel.DataAnnotations;

namespace TicketRetrievalAPI.Abstractions.Models.Database
{
    public class Tickets
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Timestamp]
        public DateTime DateTime { get; set; }

        public bool IsValid { get; set; }
    }
}
