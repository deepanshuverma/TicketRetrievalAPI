using Microsoft.EntityFrameworkCore;
using TicketRetrievalAPI.Abstractions.Models.Database;

namespace TicketRetrievalAPI.DataAccessLayer
{
    public class ApplicationDBContext:DbContext
    {
        public DbSet<Tickets> Tickets { get; set; } = null!;
        
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
    }
}
