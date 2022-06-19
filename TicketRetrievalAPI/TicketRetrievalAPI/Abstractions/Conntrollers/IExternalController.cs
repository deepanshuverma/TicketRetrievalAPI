using TicketRetrievalAPI.Abstractions.Models;

namespace TicketRetrievalAPI.Abstractions.Conntrollers
{
    public interface IExternalController
    {
        public GetNewTicketResponse getNewTickTicketResponse();

        public ValidateTicketResponse validateTicket(ValidateTicketResponse response);
    }
}
