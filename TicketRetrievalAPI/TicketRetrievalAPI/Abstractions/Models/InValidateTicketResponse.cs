namespace TicketRetrievalAPI.Abstractions.Models
{
    public class InValidateTicketResponse
    {
        public bool IsSuccess { get; set; } 

        public string? Error { get; set; }
    }
}
