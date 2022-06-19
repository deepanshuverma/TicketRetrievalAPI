using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketRetrievalAPI.Abstractions.Exceptions;
using TicketRetrievalAPI.Abstractions.Models;
using TicketRetrievalAPI.DataAccessLayer;

namespace TicketRetrievalAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InternalController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public InternalController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/GetCountOfAllIssuedTcikets")]
        public async Task<TicketIssuedCountResponse> GetCountOfAllIssuedTcikets()
        {
            var count = await _context.Tickets.CountAsync();
            var respone = new TicketIssuedCountResponse()
            {
                count = count
            };
            return respone;
        }

        [HttpPost]
        [Route("/InValidateTicket")]
        public async Task<InValidateTicketResponse> InValidateTicket(ValidateTicketRequest request)
        {
            try
            {
                var response = new InValidateTicketResponse();
                var searchResult = await _context.Tickets.SingleOrDefaultAsync(i => i.Id == Guid.Parse(request.Id));
                if (searchResult != null)
                {
                    searchResult.IsValid = false;
                    await _context.SaveChangesAsync();
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Error = $"{request.Id} is Invalid";
                    throw new KeyNotFoundException(response.Error);
                }

                return response;
            } catch (Exception e)
            {
                throw new TicketingExceptions("Invalid guid is passed");
            }
            
        }

    }
}
