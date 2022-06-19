using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketRetrievalAPI.Abstractions.Models;
using TicketRetrievalAPI.Abstractions.Models.Database;
using TicketRetrievalAPI.DataAccessLayer;

namespace TicketRetrievalAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ExternalController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/CreateNewTicket")]
        public async Task<GetNewTicketResponse> CreateNewTciket()
        {
            var ticket = new Tickets
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                IsValid = true
            };
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            var respone = new GetNewTicketResponse()
            {
                Id = ticket.Id
            };
            return respone;
        }

        [HttpPost]
        [Route("/ValidateTicket")]
        public async Task<ValidateTicketResponse> ValidateTicket(ValidateTicketRequest request)
        {
            var response = new ValidateTicketResponse();
            if (await TicketsExists(request.Id))
            {
                response.IsValid = true;
            }
            return response;
        }


        private async Task<bool> TicketsExists(Guid id) => await _context.Tickets.AnyAsync(e => e.Id == id);
    }
}
